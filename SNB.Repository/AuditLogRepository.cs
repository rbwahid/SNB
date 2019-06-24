using SNB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Repository
{
    public class AuditLogRepository
    {
        private readonly SNBDbContext _context;

        public AuditLogRepository(SNBDbContext context)
        {
            _context = context;
        }
        
        public IEnumerable<AuditLog> GetAllAuditLogs(int pageNo, int pageSize, DateTime? dateTo, DateTime? dateFrom, string selectedAction, string selectedModule, int? userId)
        {
            var fromDate = dateFrom ?? DateTime.Now.AddDays(-7).Date;
            var toDate = (dateTo ?? DateTime.Now).AddDays(1);
            return _context.AuditLogs.Where(x => (fromDate == null || x.UpdatedDate >= fromDate) && 
                     (toDate == null || x.UpdatedDate <= toDate) && (userId == null || x.CreatedUser == userId) && 
                     ((selectedModule == null || selectedModule == "") || x.TableName.ToUpper() == selectedModule.ToUpper()) && 
                    ((selectedAction == null || selectedAction == "") || x.EventType.ToUpper() == selectedAction.ToUpper()))
                    .OrderByDescending(o => o.UpdatedDate).Skip(pageNo * pageSize).Take(pageSize);
        }

        public IEnumerable<AuditLog> GetAllAuditLogs()
        {
            return _context.AuditLogs;
        }

        public List<string> GetAllActions()
        {
            return _context.AuditLogs.ToList().GroupBy(x => x.EventType).Select(y => y.Key).ToList();
        }

        public List<string> GetAllModules()
        {
            return _context.AuditLogs.ToList().GroupBy(x => x.TableName).Select(y => y.Key).ToList();
        }
    }
}
