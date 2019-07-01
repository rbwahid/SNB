using SNB.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Repository
{
    public class AttachmentFileRepository : BaseRepository<AttachmentFile>
    {
        private readonly SNBDbContext _context;
        public AttachmentFileRepository(SNBDbContext context) : base(context)
        {
            _context = context;
        }

       
    }
}
