using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Repository
{
    public class AreatUnitOfwork :IDisposable
    {
        private SNBDbContext _context { get; set; }
        private AreaRepository _AreaRepository { get; set; }

        public AreatUnitOfwork(SNBDbContext context)
        {
            _context = context;
            _AreaRepository = new AreaRepository(_context);
        }

        public AreaRepository AreaRepository => _AreaRepository;

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
