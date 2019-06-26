using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SNB.Entities
{
    [Table("PropertyTypes")]
    public class PropertyType : Entity
    {
        [Required]
        public string TypeName { get; set; }
    }
}