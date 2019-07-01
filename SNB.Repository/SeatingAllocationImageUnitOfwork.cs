using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Repository
{
    public class SeatingAllocationImageUnitOfwork : IDisposable
    {
        private SNBDbContext _context { get; set; }
        private SeatingAllocationImageRepository _seatingAllocationImageRepository  { get; set; }

        public SeatingAllocationImageUnitOfwork(SNBDbContext context)
        {
            _context = context;
            _seatingAllocationImageRepository = new SeatingAllocationImageRepository(_context);
        }

        public SeatingAllocationImageRepository SeatingAllocationImageRepository => _seatingAllocationImageRepository;

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
