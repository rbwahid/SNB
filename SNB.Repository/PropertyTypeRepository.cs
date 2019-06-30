using SNB.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Repository
{
    public class PropertyTypeRepository : BaseRepository<PropertyType>
    {
        private readonly SNBDbContext _context;
        public PropertyTypeRepository(SNBDbContext context) : base(context)
        {
            _context = context;
        }

        public bool IsTypeNameExist(string TypeName, string InitialTypeName)
        {
            bool isNotExist = true;

            if (TypeName != string.Empty && InitialTypeName == "undefined")
            {
                var isExist = _context.PropertyTypes.Any(x => !x.IsDeleted && x.TypeName.ToLower().Equals(TypeName.ToLower()));

                if (isExist)
                {
                    isNotExist = false;
                }
            }

            if (TypeName != string.Empty && InitialTypeName != "undefined")
            {
                var isExist = _context.PropertyTypes.Any(x => !x.IsDeleted && x.TypeName.ToLower() == TypeName.ToLower() && x.TypeName.ToLower() != InitialTypeName.ToLower());

                if (isExist)
                {
                    isNotExist = false;
                }
            }

            return isNotExist;
        }
    }
}
