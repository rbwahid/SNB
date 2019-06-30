﻿using SNB.Entities;
using SNB.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNB.Web.Models
{
    public class UserRoleModel : UserRole
    {
        private UserRoleService _roleService { get; set; }

        public List<RoleTaskCheckBoxModel> RoleTaskList { get; set; }

        [Required]
        [Remote("IsRoleNameExist", "User", AdditionalFields = "InitialRoleName", ErrorMessage = "Role already Exist")]
        [Display(Name = "Role")]
        public new string RoleName
        {
            get { return base.RoleName; }
            set { base.RoleName = value; }
        }

        public int loggedInUserId { get; set; }

        public UserRoleModel()
        {
            loggedInUserId = AuthenticatedUser.GetUserFromIdentity().UserId;
            _roleService = new UserRoleService();
            this.RoleTaskList = new RoleTaskCheckBoxList().TaskList.OrderBy(x => x.PermissionCategory).ThenBy(x => x.PermissionName).ToList();
        }

        public List<UserRole> GetAllRoles()
        {
            return _roleService.GetAllRoles().ToList();
        }

        public UserRoleModel(int id) : this()
        {
            var role = _roleService.GetRole(id);
            this.Id = role.Id;
            this.RoleName = role.RoleName;
            this.Status = role.Status;
            this.RoleTaskList = new List<RoleTaskCheckBoxModel>();
            foreach (var item in new RoleTaskCheckBoxList().TaskList.OrderBy(x => x.PermissionCategory).ThenBy(x => x.PermissionName))
            {
                item.IsChecked = role.RolePermissionCollection.Any(x => x.Permission.Equals(item.PermissionName));
                this.RoleTaskList.Add(item);
            }
        }

        public void AddRole()
        {
            var taskList = this.RoleTaskList.Where(x => x.IsChecked).Select(taskItem => new UserRolePermission
            {
                Permission = taskItem.PermissionName
            }).ToList();
            _roleService.AddRole(RoleName, taskList, loggedInUserId);
        }

        public void EditRole(int id)
        {
            var taskList = this.RoleTaskList.Where(x => x.IsChecked).Select(taskItem => new UserRolePermission
            {
                Permission = taskItem.PermissionName
            }).ToList();
            _roleService.EditRole(id, RoleName, taskList, loggedInUserId);
        }

        public void DeleteRole(int id)
        {
            _roleService.DeleteRole(id, loggedInUserId);
        }

        public bool IsRoleNameExist(string RoleName, string InitialRoleName)
        {
            return _roleService.IsRoleNameExist(RoleName, InitialRoleName);
        }

    }

    #region role viewmodels
    public class RoleTaskCheckBoxModel
    {
        public string PermissionName { get; set; }
        public string PermissionCategory { get; set; }
        public bool IsChecked { get; set; }

    }

    public class RoleTaskCheckBoxList
    {
        public List<RoleTaskCheckBoxModel> TaskList = new List<RoleTaskCheckBoxModel>
        {

            new RoleTaskCheckBoxModel {PermissionName="User_Configuration", PermissionCategory = "Configuration"},
            new RoleTaskCheckBoxModel {PermissionName="Role_Configuration", PermissionCategory = "Configuration"},

            //new RoleTaskCheckBoxModel {PermissionName="Property_View", PermissionCategory = "Property"},
            //new RoleTaskCheckBoxModel {PermissionName="Property_Create", PermissionCategory = "Property"},
            //new RoleTaskCheckBoxModel {PermissionName="Property_Edit", PermissionCategory = "Property"},
            //new RoleTaskCheckBoxModel {PermissionName="Property_Delete", PermissionCategory = "Property"},
            //new RoleTaskCheckBoxModel {PermissionName="Property_Report", PermissionCategory = "Property"},

            //new RoleTaskCheckBoxModel {PermissionName="Seating_Allocation_View", PermissionCategory = "SeatingAllocation"},
            //new RoleTaskCheckBoxModel {PermissionName="Seating_Allocation_Create", PermissionCategory = "SeatingAllocation"},
            //new RoleTaskCheckBoxModel {PermissionName="Seating_Allocation_Edit", PermissionCategory = "SeatingAllocation"},
            //new RoleTaskCheckBoxModel {PermissionName="Seating_Allocation_Delete", PermissionCategory = "SeatingAllocation"},
            //new RoleTaskCheckBoxModel {PermissionName="Seating_Allocation_Report", PermissionCategory = "SeatingAllocation"},

            //new RoleTaskCheckBoxModel {PermissionName="Property_Booking_View", PermissionCategory = "PropertyBooking"},
            //new RoleTaskCheckBoxModel {PermissionName="Property_Booking_Create", PermissionCategory = "PropertyBooking"},
            //new RoleTaskCheckBoxModel {PermissionName="Property_Booking_Edit", PermissionCategory = "PropertyBooking"},
            //new RoleTaskCheckBoxModel {PermissionName="Property_Booking_Delete", PermissionCategory = "PropertyBooking"},
            //new RoleTaskCheckBoxModel {PermissionName="Property_Booking_Report", PermissionCategory = "PropertyBooking"},

        };


    }
    #endregion
}