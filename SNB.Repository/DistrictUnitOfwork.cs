using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Repository
{
    public class DistrictUnitOfwork :IDisposable
    {
        private SNBDbContext _context { get; set; }
        private DistrictRepository _DistrictRepository { get; set; }

        public DistrictUnitOfwork(SNBDbContext context)
        {
            _context = context;
            _DistrictRepository = new DistrictRepository(_context);
        }

        public DistrictRepository DistrictRepository => _DistrictRepository;

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
