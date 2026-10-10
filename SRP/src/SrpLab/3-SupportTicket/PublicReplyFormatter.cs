using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._3_SupportTicket
{


    /// <summary>
    /// Formats customer-facing replies for support tickets.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when the format or content of the customer-facing reply changes.
    /// </remarks>
    public sealed class PublicReplyFormatter
    {
        public string Format(
            string ticketId,
            string priority,
            string agentName,
            DateTimeOffset slaDeadline)
        {
            var apology = priority == "P1"
                ? "We are treating this as a critical incident."
                : "Thanks for reaching out.";

            return $"Hi,\n" +
                   $"{apology}\n" +
                   $"Ticket {ticketId} is with {agentName}. " +
                   $"Next update before {slaDeadline:u}.\n";
        }
    }
}
