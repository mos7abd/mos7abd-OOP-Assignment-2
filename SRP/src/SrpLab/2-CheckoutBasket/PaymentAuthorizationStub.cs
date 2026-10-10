using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._2_CheckoutBasket
{

    /// <summary>
    /// Generates a fake payment authorization code for testing purposes.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when the way the test payment authorization code is generated changes.
    /// </remarks>
    public sealed class PaymentAuthorizationStub
    {
        public string Authorize(
            decimal grandTotal,
            string cardLast4,
            int lineCount)
        {
            var payload = $"{grandTotal:0.00}|{cardLast4}|{lineCount}";
            var hash = payload.GetHashCode();

            return $"AUTH-{Math.Abs(hash):X8}";
        }
    }
}
