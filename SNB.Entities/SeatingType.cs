using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Entities
{
    [Table("SeatingTypes")]
    public class SeatingType:Entity
    {
        [Required]
        public string TypeName { get; set; }
    }
}
