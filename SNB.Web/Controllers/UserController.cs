using SNB.Common;
using SNB.Web.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SNB.Web.Controllers
{
    [Authorize]
    //[CustomExceptionFilter]
    public class UserController : Controller
    {
        [Roles("Global_SupAdmin,User_Configuration")]
        public ActionResult Index()
        {
            return View(new UserModel().GetAllUser().ToList());
        }

        public ActionResult Details(int id)
        {
            var model = new UserModel().GetUserById(id);
            return PartialView("_Details", model);
        }

        public JsonResult ResetPassword(int id)
        {
            string str = "";
            try
            {
                new UserModel().ResetPassword(id);
                str = "Password Reset Successful";
            }
            catch (Exception r)
            {
                str = r.Message;
            }
            return Json(new { msg = str });
        }

        #region login logout area
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string ReturnUrl)
        {

            if (!User.Identity.IsAuthenticated)
            {
                ViewBag.ReturnUrl = ReturnUrl;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserLoginModel model, string ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {
                TempData["PasswordAgedMessage"] = "";
                var loginModel = new UserLoginModel();
                var user = loginModel.GetUserByUsername(model.UserName);
                if (user != null)
                {
                    MD5 md5Hash = MD5.Create();
                    string hashPassword = EncryptDecrypt.GetMd5Hash(md5Hash, model.Password);
                    var validUser = loginModel.GetValidUserByPassword(model.UserName, hashPassword);
                    if (validUser == null)
                    {
                        ModelState.AddModelError("", "Invalid credentials. Please try again.");
                    }
                    else if (validUser.Status == (int)EnumUserStatus.Pending_User)
                    {
                        ModelState.AddModelError("", "You are not eligible for access, please wait for approve by admin or contact with admin");
                    }
                    else
                    {

                        if (loginModel.IsFirstLogin(validUser.Id))
                        {

                            TempData["PasswordAgedMessage"] = "Please change password at first login.";
                            return RedirectToAction("ChangePassword", "User", new { userName = user.UserName.ToLower() });
                        }

                        FormsAuthentication.SetAuthCookie(validUser.Id + "|" + validUser.UserName.ToUpper() + "|" + validUser.FullName + "|" + validUser.ImageFile + "|" + validUser.UserType, model.RememberMe); ;
                        Session["sessionid"] = System.Web.HttpContext.Current.Session.SessionID;
                        loginModel.SaveLogin(validUser.UserName.ToUpper(), System.Web.HttpContext.Current.Session.SessionID.ToString(), true);

                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
                ModelState.Remove("Password");
            }
            return View(model);
        }
       
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #endregion
        
        #region user register area
        //[Roles("Global_SupAdmin,User_Creation")]

        [Roles("Global_SupAdmin,User_Configuration")]
        public ActionResult Register()
        {

            ViewBag.Message = "";
            TempData["RegistrationSuccess"] = "";
            //ViewBag.RoleId = new SelectList(db.Roles.Where(r => r.Status == 1).OrderBy(x => x.RoleName), "RoleId", "RoleName");
            UserRegisterModel registerModel = new UserRegisterModel();

            return View(registerModel);
        }

        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserRegisterModel userRegister)
        {
            ViewBag.Message = "";
            if (ModelState.IsValid)
            {

                userRegister.RegisterNewUser();
                TempData["RegistrationSuccess"] = "New user registration successfully complete! Username and Password sent to user by Email.";

                return RedirectToAction("Index","User");
            }
            else
            {
                ViewBag.Message = "Something went wrong! please try again";
            }
            //ViewBag.RoleId = new SelectList(db.Roles.Where(r => r.RoleId != 1 && r.Status == 1).OrderBy(x => x.RoleName), "RoleId", "RoleName", userRegister.RoleId);
            return View(userRegister);
        }
        #endregion

        #region user edit area
        [Roles("Global_SupAdmin,User_Configuration")]
        public ActionResult Edit(int id)
        {

            ViewBag.Message = "";
            TempData["RegistrationSuccess"] = "";
            UserRegisterModel registerModel = new UserRegisterModel(id);

            return View(registerModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserRegisterModel userRegister)
        {
            ViewBag.Message = "";

            userRegister.EditUser();
            TempData["RegistrationSuccess"] = "User update successfully complete!";

            return RedirectToAction("index");
            //ViewBag.RoleId = new SelectList(db.Roles.Where(r => r.RoleId != 1 && r.Status == 1).OrderBy(x => x.RoleName), "RoleId", "RoleName", userRegister.RoleId);

        }
        #endregion

        #region user password change area
        [AllowAnonymous]
        public ActionResult ChangePassword(string userName)
        {
            ViewBag.PasswordAged = "";
            ViewBag.oldPasswordNotMatched = "";
            ViewBag.PasswordHistoryAlert = "";
            ChangePsswordViewModel model = new ChangePsswordViewModel();
            if (userName != null)
            {
                model.UserName = userName;
            }
            else
            {
                model.UserName = AuthenticatedUser.GetUserFromIdentity().Username;
            }
            ViewBag.PasswordAged = TempData["PasswordAgedMessage"];
            return View(model);
        }

        // POST: /Account/ChangePassword
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ChangePassword(ChangePsswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    if (model.NewPassword == model.OldPassword)
                    {
                        changePasswordSucceeded = false;
                    }
                    else
                    {
                        MD5 md5Hash = MD5.Create();
                        string hashOldPassword = EncryptDecrypt.GetMd5Hash(md5Hash, model.OldPassword);
                        string hashNewPassword = EncryptDecrypt.GetMd5Hash(md5Hash, model.NewPassword);


                        var user = model.GetValidUserByPassword(model.UserName, hashOldPassword);

                        if (user == null)
                        {
                            ViewBag.oldPasswordNotMatched = "Wrong Password!";
                            changePasswordSucceeded = false;
                        }


                        //if (CheckPasswordStrength(user.UserName, user.FullName, model.NewPassword) && hashNewPassword != user.PrevLastPassword && hashNewPassword != user.LastPassword)
                        //{
                        else
                        {
                            model.SaveNewPassword(model.UserName, hashNewPassword);
                            changePasswordSucceeded = true;
                        }
                        //}
                        //else
                        //{
                        //    ViewBag.PasswordHistoryAlert = "You can not use previously used password or Password should not be part of Name!";
                        //    changePasswordSucceeded = false;
                        //}
                    }
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded == true)
                {
                    if (!User.Identity.IsAuthenticated)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        return RedirectToAction("ChangePasswordSuccess", "User");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Current password is incorrect or new password is invalid.");
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess
        [AllowAnonymous]
        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }
        #endregion

        #region user roles configuration
        [Roles("Global_SupAdmin,Role_Configuration")]
        public ActionResult Roles()
        {
            var roles = new UserRoleModel().GetAllRoles().ToList();
            return View("RoleList", roles);
        }

        [Roles("Global_SupAdmin,Role_Configuration")]
        public ActionResult AddRole()
        {
            return View(new UserRoleModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRole(UserRoleModel model)
        {
            if (ModelState.IsValid)
            {
                model.AddRole();
                return RedirectToAction("Roles");
            }
            //try
            //{
            //    roleModel.AddRole();
            //    TempData["message"] = "Successfully added Role.";
            //    TempData["alertType"] = "success";
            //}

            //catch (Exception e)
            //{
            //    TempData["message"] = "Failed to Add Role.";
            //    TempData["alertType"] = "danger";
            //    Console.Write(e.Message);
            //}
            return View(model);
        }

        [Roles("Global_SupAdmin,Role_Configuration")]
        public ActionResult EditRole(int id)
        {
            var role = new UserRoleModel(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(UserRoleModel model)
        {
            if (ModelState.IsValid)
            {
                model.EditRole(model.Id);
                return RedirectToAction("Roles");
            }
            return View(model);
        }
        #endregion

        #region User Specific

        #region user registrtion by user
        [AllowAnonymous]
        public ActionResult UserRegistration()
        {

            ViewBag.Message = "";
            TempData["RegistrationSuccess"] = "";
            //ViewBag.RoleId = new SelectList(db.Roles.Where(r => r.Status == 1).OrderBy(x => x.RoleName), "RoleId", "RoleName");
            RegistrationByUserModel registerModel = new RegistrationByUserModel();

            return View(registerModel);
        }

        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> UserRegistration(RegistrationByUserModel userRegister)
        {
            ViewBag.Message = "";
            if (ModelState.IsValid)
            {

                userRegister.RegisterNewUser();
                TempData["RegistrationSuccess"] = "New user registration successfully complete! Username and Password sent to user by Email.";

                return RedirectToAction("Login", "User");
            }
            else
            {
                ViewBag.Message = "Something went wrong! please try again";
            }
            //ViewBag.RoleId = new SelectList(db.Roles.Where(r => r.RoleId != 1 && r.Status == 1).OrderBy(x => x.RoleName), "RoleId", "RoleName", userRegister.RoleId);
            return View(userRegister);
        }
        #endregion

        #region user profile edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserProfileEdit(RegistrationByUserModel userRegister)
        {

            ViewBag.Message = "";

            userRegister.EditUser();
            TempData["RegistrationSuccess"] = "User update successfully complete!";

            return RedirectToAction("UserProfile");
        }

        #endregion

        #region user profile
        [HttpGet]
        public ActionResult UserProfile()
        {
            return View();
        }

        #endregion

        #region user password change
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserPasswordChange(ChangePsswordViewModel model)
        {
                
            MD5 md5Hash = MD5.Create();
            string hashNewPassword = EncryptDecrypt.GetMd5Hash(md5Hash, model.NewPassword);
            model.SaveNewPassword(AuthenticatedUser.GetUserFromIdentity().UserId, hashNewPassword);

            return RedirectToAction("UserProfile","User");
        }

        #endregion

        #endregion

        #region json helper
        [AllowAnonymous]
        public JsonResult IsUserNameExist(string UserName, string InitialUserName)
        {
            bool isNotExist = new UserModel().IsUserNameExist(UserName, InitialUserName);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult IsEmailExist(string Email, string InitialEmail)
        {
            bool isNotExist = new UserModel().IsEmailExist(Email, InitialEmail);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsRoleNameExist(string RoleName, string InitialRoleName)
        {
            bool isNotExist = new UserRoleModel().IsRoleNameExist(RoleName, InitialRoleName);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            new UserModel().DeleteUser(id);
            return Json(new { msg = "Success" });
        }

        public JsonResult DeleteRole(int id)
        {
            new UserRoleModel().DeleteRole(id);
            return Json(new { msg = "Success" });
        }

        public JsonResult Active(int id)
        {
            new UserModel().ActiveUser(id);
            return Json(new { msg = "Success" });
        }

        public ActionResult UserApprove(int id)
        {
            new UserModel().UserApprovedStatus(id);
            return Json(new { msg = "Success" });
        }

        #region password policy
        //public JsonResult CheckValidatePassword(string password, string userName)
        //{
        //    var user = new UserModel().GetUserByUsername(userName);
        //    var passwordPolicyConfig = new PasswordPolicyConfigModel().GetPasswordPolicyByType(user.UserType);
        //    bool minLCheck = passwordPolicyConfig.MinLegth > password.ToCharArray().Count() ? false : true;
        //    int minL = passwordPolicyConfig.MinLegth;
        //    bool maxLCheck = passwordPolicyConfig.MaxLegth <= password.ToCharArray().Count() ? false : true;
        //    int maxL = passwordPolicyConfig.MaxLegth;
        //    bool minNumberCheck = password.ToCharArray().Count(x => Char.IsDigit(x)) >= passwordPolicyConfig.MinNumber ? true : false;
        //    int? minNumber = passwordPolicyConfig.MinNumber;
        //    bool minLCharCheck = password.ToCharArray().Count(x => Char.IsLower(x)) >= passwordPolicyConfig.MinLowerCase ? true : false;
        //    int? minLChar = passwordPolicyConfig.MinLowerCase;
        //    bool minUCharCheck = password.ToCharArray().Count(x => Char.IsUpper(x)) >= passwordPolicyConfig.MinUpperCase ? true : false;
        //    int? minUChar = passwordPolicyConfig.MinUpperCase;
        //    bool minSCharCheck = password.ToCharArray().Count(x => !Char.IsLetterOrDigit(x)) >= passwordPolicyConfig.MinSpecialChar ? true : false;
        //    int? minSChar = passwordPolicyConfig.MinSpecialChar;
        //    return Json(new
        //    {
        //        minLCheck = minLCheck,
        //        minL = minL,
        //        maxLCheck = maxLCheck,
        //        maxL = maxL,
        //        minNumberCheck = minNumberCheck,
        //        minNumber = minNumber,
        //        minLCharCheck = minLCharCheck,
        //        minLChar = minLChar,
        //        minUCharCheck = minUCharCheck,
        //        minUChar = minUChar,
        //        minSCharCheck = minSCharCheck,
        //        minSChar = minSChar

        //    });
        //}
        #endregion


        #endregion

    }
}