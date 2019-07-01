using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Repository
{
    public class SeatingTypeUnitOfwork : IDisposable
    {
        private SNBDbContext _context { get; set; }
        private SeatingTypeRepository _SeatingTypeRepository { get; set; }

        public SeatingTypeUnitOfwork(SNBDbContext context)
        {
            _context = context;
            _SeatingTypeRepository = new SeatingTypeRepository(_context);
        }

        public SeatingTypeRepository SeatingTypeRepository => _SeatingTypeRepository;

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
