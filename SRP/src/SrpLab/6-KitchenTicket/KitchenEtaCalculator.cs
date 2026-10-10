using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._6_KitchenTicket
{

    /// <summary>
    /// Calculates the estimated preparation time for a kitchen order.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when kitchen preparation-time calculation rules change.
    /// </remarks>
    public sealed class KitchenEtaCalculator
    {
        public int Calculate(
            IEnumerable<int> prepMinutes,
            int openStations,
            bool hasAllergens)
        {
            if (openStations <= 0)
                openStations = 1;

            var preparationTimes = prepMinutes.ToList();

            var sequential = preparationTimes.Sum();

            var parallel = (int)Math.Ceiling(
                sequential / (double)openStations);

            if (hasAllergens)
                parallel += 3;

            var longest = preparationTimes.Count == 0
                ? 0
                : preparationTimes.Max();

            return Math.Max(parallel, longest);
        }
    }
}
