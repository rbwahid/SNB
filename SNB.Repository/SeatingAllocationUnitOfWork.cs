using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Repository
{
    public class SeatingAllocationUnitOfWork : IDisposable
    {
        private readonly SNBDbContext _context;
        private SeatingAllocationRepository _seatingAllocationRepository;

        public SeatingAllocationUnitOfWork(SNBDbContext context)
        {
            this._context = context;
            _seatingAllocationRepository = new SeatingAllocationRepository(context);
        }

        public SeatingAllocationRepository SeatingAllocationRepository => _seatingAllocationRepository;

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
