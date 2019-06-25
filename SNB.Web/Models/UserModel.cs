using SNB.Common;
using SNB.Entities;
using SNB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SNB.Web.Models
{
    public class UserModel : User
    {
        private UserService _userService;
        public int CurrentUserId { get; set; }

        public UserModel()
        {
            _userService = new UserService();
            
            CurrentUserId = AuthenticatedUser.GetUserFromIdentity().UserId;
        }

        public IEnumerable<User> GetAllUser()
        {
            return _userService.GetAllUsers();
        }

        public User GetUserById(int id)
        {
            return _userService.GetUserById(id);
        }

        public User GetUserByUsername(string username)
        {
            return _userService.GetUserByUsername(username);
        }

        public bool IsUserNameExist(string UserName, string InitialUserName)
        {
            return _userService.IsUserNameExist(UserName, InitialUserName);
        }

        public bool IsEmailExist(string Email, string InitialEmail)
        {
            return _userService.IsEmailExist(Email, InitialEmail);
        }

        public void DeleteUser(int id)
        {
            _userService.DeleteUser(id, CurrentUserId);
        }

        public void ActiveUser(int id)
        {
            _userService.ActiveUser(id, CurrentUserId);
        }

        public void ResetPassword(int id)
        {
            var user = _userService.GetUserById(id);
            MD5 md5Hash = MD5.Create();
            if (user != null)
            {
                string password = DefaultValue.UserResetPassword;
                _userService.ResetPassword(id, EncryptDecrypt.GetMd5Hash(md5Hash, password), CurrentUserId);
            }
        }
        
        internal List<string> GetAllModule()
        {
            return _userService.GetAllModules();
        }

        internal List<string> GetAllAction()
        {
            return _userService.GetAllActions();
        }

    }
}