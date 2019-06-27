using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Entities
{
    [Table("SeatingAllocations")]
    public class SeatingAllocation : AuditableEntity
    {
        public int SeatingTypeId { get; set; }
        [ForeignKey("SeatingTypeId")]
        public virtual SeatingType SeatingType { get; set; }
        public int PropertyId { get; set; }
        [ForeignKey("PropertyId")]
        public virtual Property Property { get; set; }
        public int? FeaturedImageId { get; set; }
        [ForeignKey("FeaturedImageId")]
        public virtual AttachementFile FeaturedImage { get; set; }
        public string Description { get; set; }
        public double Rent { get; set; }
        public DateTime? AvailableDate { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<SeatingAllocationImage> ImageCollection { get; set; }
    }
}
