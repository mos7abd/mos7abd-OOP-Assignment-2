using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._2_CheckoutBasket
{

    /// <summary>
    /// Formats the customer-facing gift message for a basket.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when the format or content of the gift message changes.
    /// </remarks>
    public sealed class GiftMessageFormatter
    {
        public string Format(
            string fromName,
            IEnumerable<string> itemSkus,
            decimal grandTotal)
        {
            var items = string.Join(", ", itemSkus);

            return $"Dear friend,\n" +
                   $"A gift from {fromName} awaits ({items}).\n" +
                   $"Total surprise value: {grandTotal:C}\n";
        }
    }
}
