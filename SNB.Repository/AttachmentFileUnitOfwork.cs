using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Repository
{
    public class AttachmentFileUnitOfwork : IDisposable
    {
        private SNBDbContext _context { get; set; }
        private AttachmentFileRepository _attachmentFileRepository { get; set; }

        public AttachmentFileUnitOfwork(SNBDbContext context)
        {
            _context = context;
            _attachmentFileRepository = new AttachmentFileRepository(_context);
        }

        public AttachmentFileRepository AttachmentFileRepository => _attachmentFileRepository;

        public void Save(string loggedInUserId)
        {
            _context.SaveChanges(loggedInUserId);
        }

        public void Save()
        {
            _context.SaveChanges("");
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
