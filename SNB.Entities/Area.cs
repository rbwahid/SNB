using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SNB.Entities
{
    [Table("Areas")]
    public class Area:Entity
    {
        [Required]
        public string AreaName { get; set; }
        public int DistrictId { get; set; }
        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }
    }
}