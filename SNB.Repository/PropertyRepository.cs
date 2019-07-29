using SNB.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNB.Common;

namespace SNB.Repository
{
    public class PropertyRepository : Repository<Property>
    {
        private readonly SNBDbContext _context;

        public PropertyRepository(SNBDbContext context) : base(context)
        {
            this._context = context;
        }

        public IEnumerable<Property> GetByUserId(int id)
        {
            return base.GetAll(x => !x.IsDeleted && x.UserId == id);
        }

        public IEnumerable<Property> GetAvailableProperty()
        {
            return _context.Properties.Where(x => !x.IsDeleted && 
                    x.Status != (int)EnumPropertyStatus.Not_Available)
                    .Distinct().OrderByDescending(y => y.CreatedAt);
        }
    }
}
