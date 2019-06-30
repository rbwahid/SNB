using SNB.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Repository
{
    public class AreaRepository : BaseRepository<Area>
    {
        private readonly SNBDbContext _context;
        public AreaRepository(SNBDbContext context) : base(context)
        {
            _context = context;
        }

        public bool IsAreaNameExist(string AreaName, string InitialAreaName)
        {
            bool isNotExist = true;

            if (AreaName != string.Empty && InitialAreaName == "undefined")
            {
                var isExist = _context.Areas.Any(x => !x.IsDeleted && x.AreaName.ToLower().Equals(AreaName.ToLower()));

                if (isExist)
                {
                    isNotExist = false;
                }
            }

            if (AreaName != string.Empty && InitialAreaName != "undefined")
            {
                var isExist = _context.Areas.Any(x => !x.IsDeleted && x.AreaName.ToLower() == AreaName.ToLower() && x.AreaName.ToLower() != InitialAreaName.ToLower());

                if (isExist)
                {
                    isNotExist = false;
                }
            }

            return isNotExist;
        }
    }
}
