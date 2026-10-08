using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._3_SupportTicket
{

    /// <summary>
    /// Calculates SLA deadlines and determines whether a ticket has breached its SLA.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when the SLA calculation or breach rules change.
    /// </remarks>
    public sealed class SlaCalculator
    {
        public DateTimeOffset CalculateDeadline(
            DateTimeOffset openedAt,
            string priority)
        {
            var hours = priority switch
            {
                "P1" => 4,
                "P2" => 24,
                _ => 72
            };

            return openedAt.AddHours(hours);
        }

        public bool IsBreached(
            DateTimeOffset openedAt,
            string priority,
            DateTimeOffset now)
        {
            return now > CalculateDeadline(openedAt, priority);
        }
    }
}
