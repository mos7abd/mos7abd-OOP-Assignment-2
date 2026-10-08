using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._7_SubscriptionBilling
{

    /// <summary>
    /// Tracks the number of failed subscription payments.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when failed-payment tracking or dunning-state rules change.
    /// </remarks>
    public sealed class FailedPaymentTracker
    {
        public int FailedPayments { get; private set; }

        public void RegisterFailedPayment()
        {
            FailedPayments++;
        }
    }
}
