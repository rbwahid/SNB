using SNB.Common;
using SNB.Entities;
using SNB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Services
{
    public class UserService
    {
        private SNBDbContext _context;
        private UserUnitOfWork _userUnitOfWork;
        private UserRoleUnitOfWork _userRoleUnitOfWork;

        public UserService()
        {
            _context = new SNBDbContext();
            _userUnitOfWork = new UserUnitOfWork(_context);
            _userRoleUnitOfWork = new UserRoleUnitOfWork(_context);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userUnitOfWork.UserRepository.GetAllUsers();
        }

        public List<AuditLog> GetAllAuditLogsByUserIdDate(int currentUserId, DateTime lastDate)
        {
            return _userUnitOfWork.UserRepository.GetAllAuditLogsByUserIdDate(currentUserId, lastDate);
        }

        public void AddUser(User user)
        {
            User newUser = new User
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                NationalID = user.NationalID,
                Gender = user.Gender,
                Address = user.Address,
                Mobile = user.Mobile,
                SupUser = user.SupUser,
                UserType = user.UserType,
                ImageFile = user.ImageFile,
                RoleId = user.RoleId,
                LastPassword = user.Password,
                LastPassChangeDate = DateTime.Now,
                PasswordChangedCount = user.PasswordChangedCount??0,
                LockoutEnabled = true,
                LockoutEndDateUtc = DateTime.Now,
                AccessFailedCount = 0,

                Status = user.Status,
                CreatedBy = user.CreatedBy,
                CreatedAt = user.CreatedAt,
            };
            _userUnitOfWork.UserRepository.Add(newUser);
            _userUnitOfWork.Save(user.CreatedBy.ToString());
        }

        public void SaveLogin(string userId, string sessionId, bool loggedIn)
        {
            LoginRecord login = new LoginRecord
            {
                UserId = userId,
                SessionId = sessionId,
                LoggedIn = loggedIn,
                LoggedInDateTime = DateTime.Now
            };
            _userUnitOfWork.LoginRecordRepository.Add(login);
            _userUnitOfWork.Save();
        }

        public void EditUser(User updateUser)
        {
            User userEntry = _userUnitOfWork.UserRepository.GetUserById(updateUser.Id);
            if (userEntry != null)
            {
                userEntry.FullName = updateUser.FullName;
                userEntry.UserName = updateUser.UserName;
                userEntry.Email = updateUser.Email;
                userEntry.Gender = updateUser.Gender;
                userEntry.Address = updateUser.Address;
                userEntry.Mobile = updateUser.Mobile;
                userEntry.RoleId = updateUser.RoleId??userEntry.RoleId;
                userEntry.UpdatedAt = updateUser.UpdatedAt;
                userEntry.UpdatedBy = updateUser.UpdatedBy;

                if (updateUser.ImageFile != null)
                {
                    userEntry.ImageFile = updateUser.ImageFile;
                }

            }

            _userUnitOfWork.Save(updateUser.UpdatedBy.ToString());
        }

        public IEnumerable<AuditLog> GetAllAuditLogs()
        {
            return _userUnitOfWork.AuditLogRepository.GetAllAuditLogs();
        }

        public List<string> GetAllActions()
        {
            return _userUnitOfWork.AuditLogRepository.GetAllActions();
        }

        public List<string> GetAllModules()
        {
            return _userUnitOfWork.AuditLogRepository.GetAllModules();
        }

        public IEnumerable<AuditLog> GetAllAuditLogs(int pageNo, int pageSize, DateTime? dateTo, DateTime? dateFrom, string selectedAction, string selectedModule, int? userId)
        {
            return _userUnitOfWork.AuditLogRepository.GetAllAuditLogs(pageNo, pageSize, dateTo, dateFrom, selectedAction, selectedModule, userId);
        }

        public string[] GetRolesById(int id)
        {
            // return _userUnitOfWork.UserRepository.GetUserById(id).UserRole.RolePermissions.Select(e=>e.Permission).ToArray<string>();
            return _userRoleUnitOfWork.RoleRepository.GetRolesById(id);
        }

        public IEnumerable<User> GetAllUser()
        {
            return _userUnitOfWork.UserRepository.GetAll();
        }

        public User GetUserById(int id)
        {
            return _userUnitOfWork.UserRepository.GetById(id);
        }

        public User GetUserByUsername(string username)
        {
            return _userUnitOfWork.UserRepository.GetUserByUsername(username);
        }

        public bool CheckUsernameIsValid(string username)
        {
            return _userUnitOfWork.UserRepository.CheckUsernameIsValid(username);
        }

        public User GetValidUserByPassword(string username, string password)
        {
            return _userUnitOfWork.UserRepository.GetValidUserByPassword(username, password);
        }

        public bool IsUserNameExist(string UserName, string InitialUserName)
        {
            return _userUnitOfWork.UserRepository.IsUserNameExist(UserName, InitialUserName);
        }

        public bool IsEmailExist(string Email, string InitialEmail)
        {
            return _userUnitOfWork.UserRepository.IsEmailExist(Email, InitialEmail);
        }

        public void DeleteUser(int id, int authorizeId)
        {
            _userUnitOfWork.UserRepository.Disable(id);
            _userUnitOfWork.Save(authorizeId.ToString());
        }

        public void ActiveUser(int id, int authorizeId)
        {
            _userUnitOfWork.UserRepository.Enable(id);
            _userUnitOfWork.Save(authorizeId.ToString());
        }

        public bool IsLockedOut(int id)
        {
            return _userUnitOfWork.UserRepository.IsLockedOut(id);
        }

        public bool GetLockoutEnabled(int id)
        {
            return _userUnitOfWork.UserRepository.GetLockoutEnabled(id);
        }

        #region password policy
        //public bool IsPasswordValidityExpired(int id)
        //{
        //    return _userUnitOfWork.UserRepository.IsPasswordValidityExpired(id);
        //}
        //public void AccessFailed(int id)
        //{
        //    _userUnitOfWork.UserRepository.AccessFailed(id);
        //}
        //public int GetMaxAccesFailedAttempt(int id)
        //{
        //    return _userUnitOfWork.UserRepository.GetMaxAccesFailedAttempt(id);
        //}
        #endregion

        public bool IsFirstLogin(int id)
        {
            return _userUnitOfWork.UserRepository.IsFirstLogin(id);
        }

        public void ResetAccesFailedCount(int Id)
        {
            var user = _userUnitOfWork.UserRepository.GetById(Id);
            user.AccessFailedCount = 0;
            _userUnitOfWork.Save();
        }

        public void SaveNewPassword(string userName, string hashNewPassword)
        {
            var user = _userUnitOfWork.UserRepository.GetUserByUsername(userName);
            user.LastPassword = user.Password;
            user.Password = hashNewPassword;
            user.LastPassChangeDate = DateTime.Now;
            user.PasswordChangedCount += 1;
            _userUnitOfWork.Save();
        }

        public void SaveNewPassword(int userId, string hashNewPassword)
        {
            var user = _userUnitOfWork.UserRepository.GetById(userId);
            user.LastPassword = user.Password;
            user.Password = hashNewPassword;
            user.LastPassChangeDate = DateTime.Now;
            user.PasswordChangedCount += 1;
            _userUnitOfWork.Save();
        }

        #region logins record
        public bool IsYourLoginStillTrue(string userId, string sid)
        {
            return _userUnitOfWork.LoginRecordRepository.IsYourLoginStillTrue(userId, sid);
        }

        public bool IsUserLoggedOnElsewhere(string userId, string sid)
        {
            return _userUnitOfWork.LoginRecordRepository.IsUserLoggedOnElsewhere(userId, sid);
        }

        public void LogEveryoneElseOut(string userId, string sid)
        {
            _userUnitOfWork.LoginRecordRepository.LogEveryoneElseOut(userId, sid);
        }
        #endregion

        public void ResetPassword(int id, string encryptPassword, int authorizeId)
        {
            var userEntry = GetUserById(id);
            if (userEntry != null)
            {
                userEntry.Password = encryptPassword;
                userEntry.PasswordChangedCount = 0;
                _userUnitOfWork.UserRepository.Update(userEntry);
                _userUnitOfWork.Save(authorizeId.ToString());
            }
        }

        public void Dispose()
        {
            _userUnitOfWork.Dispose();
        }
    }
}
