using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._7_SubscriptionBilling
{

    /// <summary>
    /// Formats dunning email messages for customers with failed subscription payments.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when dunning email wording or collections communication rules change.
    /// </remarks>
    public sealed class DunningEmailFormatter
    {
        public string Format(
            string customerName,
            decimal amount,
            string invoiceNumber,
            int failedPayments,
            DateOnly asOf)
        {
            var severity = failedPayments switch
            {
                <= 1 => "friendly reminder",
                2 => "second notice",
                _ => "final notice before suspension"
            };

            return $"Subject: {severity} {invoiceNumber}\n" +
                   $"Hi {customerName},\n" +
                   $"Balance {amount:C} as of {asOf:o} ({failedPayments} failures).\n";
        }
    }
}
