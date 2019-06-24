using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Entities
{
    [Table("LoginRecords")]
    public class LoginRecord : Entity
    {
        [MaxLength(1000)]
        public string UserId { get; set; }
        [MaxLength(1000)]
        public string SessionId { get; set; }
        public bool LoggedIn { get; set; }
        public DateTime? LoggedInDateTime { get; set; }
    }
}
