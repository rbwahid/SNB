using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Repository
{
    public class UserRoleUnitOfWork : IDisposable
    {
        private SNBDbContext _context { get; set; }
        private UserRoleRepository _roleRepository { get; set; }
        private RoleTaskRepository _roleTaskRepository { get; set; }

        public UserRoleUnitOfWork(SNBDbContext context)
        {
            _context = context;
            _roleRepository = new UserRoleRepository(_context);
            _roleTaskRepository = new RoleTaskRepository(_context);
        }

        public UserRoleRepository RoleRepository => _roleRepository;
        public RoleTaskRepository RoleTaskRepository => _roleTaskRepository;

        public void Save(string loggedInUserId)
        {
            _context.SaveChanges(loggedInUserId);
        }

        public void Save()
        {
            _context.SaveChanges("");
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
