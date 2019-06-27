using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Entities
{
    [Table("Properties")]
    public class Property : AuditableEntity
    {
        [Required]
        public string PropertyName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int PropertyTypeId { get; set; }
        [ForeignKey("PropertyTypeId")]
        public virtual PropertyType PropertyType { get; set; }
        public int? AreaId { get; set; }
        [ForeignKey("AreaId")]
        public virtual Area Area { get; set; }
        public int? DistrictId { get; set; }
        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }

        public virtual ICollection<PropertyImage> ImageCollection { get; set; }
        public virtual ICollection<SeatingAllocation> SeatingAllocationCollection { get; set; }
    }
}
