using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Repository
{
    public class PropertyTypeUnitOfwork : IDisposable
    {
        private SNBDbContext _context { get; set; }
        private PropertyTypeRepository _PropertyTypeRepository { get; set; }

        public PropertyTypeUnitOfwork(SNBDbContext context)
        {
            _context = context;
            _PropertyTypeRepository = new PropertyTypeRepository(_context);
        }

        public PropertyTypeRepository PropertyTypeRepository => _PropertyTypeRepository;

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
