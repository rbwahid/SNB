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
    }
}
