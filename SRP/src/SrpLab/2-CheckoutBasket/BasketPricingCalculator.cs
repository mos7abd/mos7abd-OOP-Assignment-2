using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._2_CheckoutBasket
{

    /// <summary>
    /// Calculates basket subtotal and final total.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when the rules for calculating basket prices and totals change.
    /// </remarks>
    public sealed class BasketPricingCalculator
    {
        public decimal CalculateSubTotal(
            IEnumerable<(string Sku, decimal Price, int Qty)> lines)
        {
            return lines.Sum(line => line.Price * line.Qty);
        }

        public decimal CalculateGrandTotal(
            IEnumerable<(string Sku, decimal Price, int Qty)> lines,
            decimal discountAmount,
            bool giftWrap)
        {
            var subTotal = CalculateSubTotal(lines);

            var total = subTotal - discountAmount;

            if (giftWrap)
                total += 4.99m;

            return Math.Max(0m, total);
        }
    }
}
