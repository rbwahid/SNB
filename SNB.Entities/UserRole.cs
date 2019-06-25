using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Entities
{
    [Table("UserRoles")]
    public class UserRole : AuditableEntity
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<UserRolePermission> RolePermissionCollection { get; set; }
    }
}
