using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Entities
{
    [Table("PropertyBookings")]
    public class PropertyBooking:AuditableEntity
    {
        [Display(Name= "Property")]
        public int PropertyId { get; set; }
        [ForeignKey("PropertyId")]
        public virtual Property Property { get; set; }
        public int? TotalSeat { get; set; }
        [Display(Name = "User")]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Display(Name = "Confirm Date")]
        public DateTime? ConfirmDate { get; set; }
        [Display(Name = "From Date")]
        public DateTime? FromDate { get; set; }
        [Display(Name = "To Date")]
        public DateTime? ToDate { get; set; }
        [Display(Name = "Comments")]
        public string Remarks { get; set; }

        public int? Status { get; set; }

    }
}
