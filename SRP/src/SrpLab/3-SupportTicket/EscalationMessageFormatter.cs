using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._3_SupportTicket
{


    /// <summary>
    /// Formats internal escalation messages for support tickets.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when the format or content of the internal escalation message changes.
    /// </remarks>
    public sealed class EscalationMessageFormatter
    {
        public string Format(
            string ticketId,
            string priority,
            DateTimeOffset slaDeadline)
        {
            return $"ESCALATE {ticketId} priority={priority} " +
                   $"breachAt={slaDeadline:u} keywords-scanned=yes";
        }
    }
}
