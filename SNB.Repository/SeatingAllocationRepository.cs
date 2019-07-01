using SNB.Common;
using SNB.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Repository
{
    public class SeatingAllocationRepository : Repository<SeatingAllocation>
    {
        private readonly SNBDbContext context;

        public SeatingAllocationRepository(SNBDbContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<SeatingAllocation> GetByPropertyId(int? id)
        {
            return base.GetAll(x => !x.IsDeleted && x.PropertyId == id);
        }

        public IEnumerable<SeatingAllocation> GetByUserId(int id)
        {
            return base.GetAll(x => !x.IsDeleted && x.Property.UserId == id);
        }

        public IEnumerable<SeatingAllocation> GetAvailableSeatingAllocation()
        {
            return base.GetAll(x => !x.IsDeleted && x.Status!=(int)EnumSeatingAllocationStatus.Not_Available).OrderByDescending(y => y.CreatedAt);
        }
    }
}
