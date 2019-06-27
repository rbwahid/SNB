using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SNB.Entities
{
    [Table("Districts")]
    public class District:Entity
    {
        [Required]
        public string DistrictName { get; set; }

        //public virtual ICollection<Area> AreaCollection { get; set; }
    }
}