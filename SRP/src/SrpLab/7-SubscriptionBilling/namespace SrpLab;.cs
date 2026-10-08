using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._7_SubscriptionBilling
{

    /// <summary>
    /// Generates sequential invoice numbers for subscription billing.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when invoice numbering or fiscal numbering rules change.
    /// </remarks>
    public sealed class InvoiceNumberGenerator
    {
        private static int _invoiceSeq = 1000;

        public string Generate(DateOnly periodStart)
        {
            var number = ++_invoiceSeq;

            return $"INV-{periodStart:yyyyMM}-{number:D5}";
        }
    }
}
