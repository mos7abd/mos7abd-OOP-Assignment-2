using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._7_SubscriptionBilling
{

    /// <summary>
    /// Calculates the prorated subscription amount for a billing period.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when subscription proration or billing-calendar rules change.
    /// </remarks>
    public sealed class SubscriptionProrationCalculator
    {
        public decimal Calculate(
            decimal monthlyPrice,
            DateOnly periodStart,
            DateOnly periodEnd,
            DateOnly activeFrom)
        {
            if (activeFrom <= periodStart)
                return monthlyPrice;

            if (activeFrom >= periodEnd)
                return 0m;

            var totalDays = periodEnd.DayNumber - periodStart.DayNumber;

            if (totalDays <= 0)
                return monthlyPrice;

            var used = periodEnd.DayNumber - activeFrom.DayNumber;

            return Math.Round(
                monthlyPrice * used / totalDays,
                2);
        }
    }
}
