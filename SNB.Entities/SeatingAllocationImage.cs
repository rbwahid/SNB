using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Entities
{
    [Table("SeatingAllocationImages")]
    public class SeatingAllocationImage:Entity
    {
        public int SeatingAllocationId { get; set; }
        [ForeignKey("SeatingAllocationId")]
        public virtual SeatingAllocation SeatingAllocation { get; set; }
        public int AttachementFileId { get; set; }
        [ForeignKey("AttachementFileId")]
        public virtual AttachmentFile AttachementFile { get; set; }
    }
}
