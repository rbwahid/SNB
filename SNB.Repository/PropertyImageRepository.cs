using SNB.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Repository
{
    public class PropertyImageRepository : BaseRepository<PropertyImage>
    {
        private readonly SNBDbContext _context;
        public PropertyImageRepository(SNBDbContext context) : base(context)
        {
            _context = context;
        }


        public void AddRange(ICollection<PropertyImage> imageCollection)
        {
            _context.PropertyImages.AddRange(imageCollection);
        }
    }
}
