using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Repository
{
    public class PropertyUnitOfWork : IDisposable
    {
        private readonly SNBDbContext _context;
        private PropertyRepository _propertyRepository;

        public PropertyUnitOfWork(SNBDbContext context)
        {
            this._context = context;
            _propertyRepository = new PropertyRepository(context);
        }

        public PropertyRepository PropertyRepository => _propertyRepository;

        public void Save()
        {
            this._context.SaveChanges("");
        }

        public void Save(string loggedInUserId)
        {
            this._context.SaveChanges(loggedInUserId);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }
    }
}
