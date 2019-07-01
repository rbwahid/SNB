using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Repository
{
    public class PropertyBookingUnitOfWork : IDisposable
    {
        private readonly SNBDbContext _context;
        private PropertyBookingRepository _propertyBookingRepository;

        public PropertyBookingUnitOfWork(SNBDbContext context)
        {
            this._context = context;
            _propertyBookingRepository = new PropertyBookingRepository(context);
        }

        public PropertyBookingRepository PropertyBookingRepository => _propertyBookingRepository;

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
