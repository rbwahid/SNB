﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using SNB.Common;
using SNB.Entities;
using SNB.Services;

namespace SNB.Web.Models
{
    public class RegistrationByUserModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string FullName { get; set; }
        [Required]
        [Remote("IsUserNameExist", "User", ErrorMessage = "Username already Exist", AdditionalFields = "InitialUserName")]
        [Display(Name = "User  Name")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Remote("IsEmailExist", "User", ErrorMessage = "Email already Exist", AdditionalFields = "InitialEmail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }

        public string InitialUserName { get; set; }
        public string InitialEmail { get; set; }
        [Display(Name = "Mobile")]
        public string MobileNumber { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        //[Required]
        public string Gender { get; set; }
        public bool SupUser { get; set; }
        public string UserType { get; set; }
        [Required]
        [Display(Name = "NID No")]
        public string NationalID { get; set; }
        [Display(Name = "Image Link")]
        public string ImageFile { get; set; }
        [Display(Name = "Image")]
        public HttpPostedFileBase ImageFileBase { get; set; }
        [Display(Name = "User Role")]
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual UserRole UserRole { get; set; }
        public byte? Status { get; set; }
        public bool AcceptTerms { get; set; }


        private UserService _userService;
        private UserRoleService _userRoleService;
        public IEnumerable<UserRole> Roles { get; set; }

        public RegistrationByUserModel()
        {
            _userService = new UserService();
            _userRoleService = new UserRoleService();
            //Roles = _userRoleService.GetAllRoles();
        }

        public RegistrationByUserModel(int id) : this()
        {
            var userEntry = _userService.GetUserById(id);
            if (userEntry != null)
            {
                Id = userEntry.Id;
                FullName = userEntry.FullName;
                UserName = userEntry.UserName;
                Email = userEntry.Email;
                InitialUserName = UserName;
                InitialEmail = Email;
                Gender = userEntry.Gender;
                Address = userEntry.Address;
                MobileNumber = userEntry.Mobile;
                RoleId = userEntry.RoleId ?? 0;
                ImageFile = userEntry.ImageFile;
                UserType = userEntry.UserType;
                NationalID = userEntry.NationalID;
            }
        }

        public void RegisterNewUser()
        {
            MD5 md5Hash = MD5.Create();
            //var passwordPolicyConfigEntry = passwordPolicyConfigService.GetPasswordPolicyByUserType(UserType);
            //string password = passwordPolicyConfigEntry == null ? CommonMethods.RandomPassword(true, true, true, true, 9) : CommonMethods.RandomPassword(true, passwordPolicyConfigEntry.RequireUpperCase, passwordPolicyConfigEntry.RequiredNumericChar, passwordPolicyConfigEntry.RequireSpecialChar, passwordPolicyConfigEntry.MinLegth); /*CommonMethods.GeneratePassword();*/
            //string password = DefaultValue.UserPassword;
            string ImagePath = "";
            if (ImageFileBase != null)
            {
                var fileNameWithoutExt = Path.GetFileNameWithoutExtension(ImageFileBase.FileName);
                var fileExtension = Path.GetExtension(ImageFileBase.FileName);
                var finalFileName = FullName + "_Profile" + string.Format("{0:yyMMddhhmmss}", DateTime.Now) + fileExtension;
                string savePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/"), finalFileName);
                ImageFileBase.SaveAs(savePath);
                ImagePath = "~/Uploads/" + finalFileName;

            }
            //int? loggedInUserId = AuthenticatedUser.GetUserFromIdentity().UserId;
            User newUser = new User
            {
                FullName = FullName,
                UserName = UserName,
                Email = Email,
                Password = EncryptDecrypt.GetMd5Hash(md5Hash, NewPassword),
                NationalID = NationalID,
                Gender = Gender,
                Address = Address,
                Mobile = MobileNumber,
                SupUser = false,
                Status = UserType == DefaultValue.UserType.Tenant ? (int)EnumUserStatus.Approved_User : (int)EnumUserStatus.Pending_User,
                //CreatedBy = loggedInUserId,
                ImageFile = ImagePath,
                UserType = UserType,
                PasswordChangedCount = 1,
                RoleId = UserType == DefaultValue.UserType.Tenant ? 
                            _userRoleService.GetUserRoleByName(DefaultValue.Role.Tenant).Id :
                            _userRoleService.GetUserRoleByName(DefaultValue.Role.Landlord).Id ,
                //ExpireDate = EmployeeType != "Permanent" ? ExpireDate : null
            };
            _userService.AddUser(newUser);
            //new MailerModel().SendRegistationMail(Email, "New User Registration for SCHM Application", Name, UserName, password, ConfigurationManager.AppSettings["WebUrl"].ToString());
        }

        public void EditUser()
        {
            string ImagePath = "";
            int? loggedInUserId = AuthenticatedUser.GetUserFromIdentity().UserId;
            User updateUser = new User
            {
                Id = Id,
                FullName = FullName,
                UserName = UserName,
                Email = Email,
                Gender = Gender,
                Address = Address,
                Mobile = MobileNumber,
                UpdatedAt = DateTime.Now,
                UpdatedBy = loggedInUserId,
            };
            if (ImageFileBase != null)
            {
                var fileNameWithoutExt = Path.GetFileNameWithoutExtension(ImageFileBase.FileName);
                var fileExtension = Path.GetExtension(ImageFileBase.FileName);
                var finalFileName = FullName + "_Profile" + string.Format("{0:yyMMddhhmmss}", DateTime.Now) + fileExtension;
                string savePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/"), finalFileName);
                ImageFileBase.SaveAs(savePath);
                updateUser.ImageFile = "~/Uploads/" + finalFileName;
            }

            _userService.EditUserByUser(updateUser);
        }
    }
}