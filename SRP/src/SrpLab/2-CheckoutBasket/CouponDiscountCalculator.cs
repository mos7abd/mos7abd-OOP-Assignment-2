using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._2_CheckoutBasket
{

    /// <summary>
    /// Calculates the discount amount based on a coupon code and the basket subtotal.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when the rules for interpreting coupons and calculating discounts change.
    /// </remarks>
    public sealed class CouponDiscountCalculator
    {
        public decimal Calculate(string? couponText, decimal subTotal)
        {
            if (string.IsNullOrWhiteSpace(couponText))
                return 0m;

            var text = couponText.Trim().ToUpperInvariant();

            // Example: SAVE20 means 20% discount.
            if (text.StartsWith("SAVE") &&
                int.TryParse(text[4..], out var percentage) &&
                percentage is > 0 and <= 50)
            {
                return Math.Round(subTotal * percentage / 100m, 2);
            }

            // FREESHIP does not reduce the basket price.
            if (text.Contains("FREESHIP"))
                return 0m;

            // WELCOME10 gives up to $10 discount.
            if (text == "WELCOME10")
                return Math.Min(10m, subTotal);

            return 0m;
        }
    }
}
