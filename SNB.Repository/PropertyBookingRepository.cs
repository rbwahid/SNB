using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNB.Entities;

namespace SNB.Repository
{
    public class PropertyBookingRepository : Repository<PropertyBooking>
    {
        private readonly SNBDbContext _context;

        public PropertyBookingRepository(SNBDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<PropertyBooking> GetByUserId(int id)
        {
            return base.GetAll(x => !x.IsDeleted && x.UserId == id);
        }
    }
}
