using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._7_SubscriptionBilling
{

    /// <summary>
    /// Formats subscription billing data as an accounting ledger journal line.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when the accounting ledger export format changes.
    /// </remarks>
    public sealed class LedgerJournalFormatter
    {
        public string Format(
            string customerId,
            string invoiceNumber,
            decimal amount)
        {
            return $"{customerId},{invoiceNumber},{amount:0.00},AR-SUB";
        }
    }
}
