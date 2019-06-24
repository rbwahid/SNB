using SNB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Repository
{
    public class LoginRecordRepository : BaseRepository<LoginRecord>
    {
        private readonly SNBDbContext _context;

        public LoginRecordRepository(SNBDbContext context) : base(context)
        {
            _context = context;

        }

        public bool IsYourLoginStillTrue(string userId, string sid)
        {


            //IEnumerable<Logins> logins = (from i in context.Logins
            //                              where i.LoggedIn == true &&
            //                              i.UserId == userId && i.SessionId == sid
            //                              select i).AsEnumerable();
            var logins = _context.LoginRecords.Where(i => i.LoggedIn == true && i.UserId == userId && i.SessionId == sid);

            return logins.Any();

        }

        public bool IsUserLoggedOnElsewhere(string userId, string sid)
        {


            //IEnumerable<Logins> logins = (from i in context.Logins
            //                              where i.LoggedIn == true &&
            //                              i.UserId == userId && i.SessionId != sid
            //                              select i).AsEnumerable();
            var logins = _context.LoginRecords.Where(i => i.LoggedIn == true && i.UserId == userId && i.SessionId != sid);
            return logins.Any();
        }

        public void LogEveryoneElseOut(string userId, string sid)
        {

            //IEnumerable<Logins> logins = (from i in context.Logins
            //                              where i.LoggedIn == true &&
            //                              i.UserId == userId &&
            //                              i.SessionId != sid // need to filter by user ID
            //                              select i).AsEnumerable();
            var logins = _context.LoginRecords.Where(i => i.LoggedIn == true && i.UserId == userId && i.SessionId != sid);

            foreach (LoginRecord item in logins)
            {
                item.LoggedIn = false;
            }

            _context.SaveChanges();
        }

        public List<LoginRecord> GetLast7DaysLoginByUserId(string currentUserId)
        {
            DateTime dateTime = DateTime.Now.AddDays(-7);
            return _context.LoginRecords.Where(x => x.UserId == currentUserId && x.LoggedInDateTime >= dateTime).ToList();
        }
    }
}
