using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Common
{
    public enum EnumUserRoleStatus
    {
        General_User = 1,
        Super_Administrator = 2,
    }

    public enum EnumUserStatus
    {
        General_User = 1,
        Super_Administrator = 2,
        Pending_User = 3,
        Approved_User = 4,
    }

    public enum EnumPropertyStatus
    {
        Available = 1,
        Not_Available = 2,
    }

    public enum EnumPropertyBookingStatus
    {
        Pending = 1,
        Accepted = 2,
        Rejected = 3,
    }

    public enum EnumMonths
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }
}
