using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Entities
{
    [Table("UserRolePermissions")]
    public class UserRolePermission : Entity
    {
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual UserRole Role { get; set; }
        public string Permission { get; set; }
    }
}
