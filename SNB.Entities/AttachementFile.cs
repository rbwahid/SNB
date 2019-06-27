using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SNB.Entities
{
    [Table("AttachementFiles")]
    public class AttachementFile : AuditableEntity
    {
        public string FileName { get; set; }
        public string OrginalFileName { get; set; }
        public string FileExtension { get; set; }
        public string FileLocation { get; set; }
    }
}