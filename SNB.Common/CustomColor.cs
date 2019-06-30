using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Common
{
    public static class CustomColor
    {
        public static string SeatingAllocationStatus(int value)
        {
            switch (value)
            {
                case (int)EnumSeatingAllocationStatus.Available:
                    return "badge-success";
                case (int)EnumSeatingAllocationStatus.NotAvailable:
                    return "badge-warning";
                default:
                    return "badge-secondary";
            }
        }

        public static string PropertyBookingStatus(int value)
        {
            switch (value)
            {
                case (int)EnumPropertyBookingStatus.Pending:
                    return "badge-warning";
                case (int)EnumPropertyBookingStatus.Accepted:
                    return "badge-success";
                case (int)EnumPropertyBookingStatus.Rejected:
                    return "badge-danger";
                default:
                    return "badge-secondary";
            }
        }
    }
}
