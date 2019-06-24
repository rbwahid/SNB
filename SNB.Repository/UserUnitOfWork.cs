using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Repository
{
    public class UserUnitOfWork : IDisposable
    {
        private SNBDbContext _context { get; set; }
        private UserRepository _userRepository { get; set; }
        private LoginRecordRepository _loginRecordRepository { get; set; }
        private AuditLogRepository _auditLogRepository { get; set; }

        public UserUnitOfWork(SNBDbContext context)
        {
            _context = context;
            _userRepository = new UserRepository(_context);
            _loginRecordRepository = new LoginRecordRepository(_context);
            _auditLogRepository = new AuditLogRepository(_context);
        }

        public UserRepository UserRepository => _userRepository;
        public LoginRecordRepository LoginRecordRepository => _loginRecordRepository;
        public AuditLogRepository AuditLogRepository => _auditLogRepository;

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
