using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Entities
{
    [Table("PropertyImages")]
    public class PropertyImage:Entity
    {
        public int PropertyId { get; set; }
        [ForeignKey("PropertyId")]
        public virtual Property Property { get; set; }
        public int AttachementFileId { get; set; }
        [ForeignKey("AttachementFileId")]
        public virtual AttachmentFile AttachementFile { get; set; }
    }
}
