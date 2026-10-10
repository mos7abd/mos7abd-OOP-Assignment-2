using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._3_SupportTicket
{


    /// <summary>
    /// Determines support ticket priority from subject and body text.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when the rules for determining ticket priority from text change.
    /// </remarks>
    public sealed class TicketPriorityClassifier
    {
        public string Classify(string subject, string body)
        {
            var blob = (subject + " " + body).ToLowerInvariant();

            if (blob.Contains("down") ||
                blob.Contains("outage") ||
                blob.Contains("cannot login"))
            {
                return "P1";
            }

            if (blob.Contains("urgent") ||
                blob.Contains("asap") ||
                blob.Contains("blocked"))
            {
                return "P2";
            }

            return "P3";
        }
    }
}
