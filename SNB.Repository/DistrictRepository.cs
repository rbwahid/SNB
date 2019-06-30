using SNB.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Repository
{
    public class DistrictRepository : BaseRepository<District>
    {
        private readonly SNBDbContext _context;
        public DistrictRepository(SNBDbContext context) : base(context)
        {
            _context = context;
        }

        public bool IsDistrictNameExist(string DistrictName, string InitialDistrictName)
        {
            bool isNotExist = true;

            if (DistrictName != string.Empty && InitialDistrictName == "undefined")
            {
                var isExist = _context.Districts.Any(x => !x.IsDeleted && x.DistrictName.ToLower().Equals(DistrictName.ToLower()));

                if (isExist)
                {
                    isNotExist = false;
                }
            }

            if (DistrictName != string.Empty && InitialDistrictName != "undefined")
            {
                var isExist = _context.Districts.Any(x => !x.IsDeleted && x.DistrictName.ToLower() == DistrictName.ToLower() && x.DistrictName.ToLower() != InitialDistrictName.ToLower());

                if (isExist)
                {
                    isNotExist = false;
                }
            }

            return isNotExist;
        }
    }
}
