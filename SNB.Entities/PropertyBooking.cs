using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Entities
{
    [Table("PropertyBookings")]
    public class PropertyBooking:AuditableEntity
    {
        public int SeatingAllocationId { get; set; }
        [ForeignKey("SeatingAllocationId")]
        public virtual SeatingAllocation SeatingAllocation { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public DateTime? ConfirmDate { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public int? Status { get; set; }

    }
}
