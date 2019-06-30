using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Entities
{
    [Table("Users")]
    public class User : AuditableEntity
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "National ID")]
        public string NationalID { get; set; }
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        public string Gender { get; set; }
        public bool SupUser { get; set; }
        [Display(Name = "Image Link")]
        public string ImageFile { get; set; }
        public string UserType { get; set; }
        [Display(Name = "Role")]
        public int? RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual UserRole UserRole { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Last Password")]
        public string LastPassword { get; set; }
        [Display(Name = "Last Password Change Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LastPassChangeDate { get; set; }
        public int? PasswordChangedCount { get; set; }
        //public string SecurityStamp { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int? AccessFailedCount { get; set; }

        public int? Status { get; set; }

        [NotMapped]
        public new int? CreatedByUser { get; set; }
        [NotMapped]
        public new int? UpdatedByUser { get; set; }

    }
}
