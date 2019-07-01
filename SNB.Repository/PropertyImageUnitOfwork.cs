using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Repository
{
    public class PropertyImageUnitOfwork : IDisposable
    {
        private SNBDbContext _context { get; set; }
        private PropertyImageRepository _propertyImageRepository { get; set; }

        public PropertyImageUnitOfwork(SNBDbContext context)
        {
            _context = context;
            _propertyImageRepository = new PropertyImageRepository(_context);
        }

        public PropertyImageRepository PropertyImageRepository => _propertyImageRepository;

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
