using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._8_WarehousePickList
{

    /// <summary>
    /// Calculates how much stock can be allocated for each warehouse item.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when stock allocation or backorder rules change.
    /// </remarks>
    public sealed class StockAllocationCalculator
    {
        public IReadOnlyList<(string Sku, int Allocated)> Calculate(
            IEnumerable<(string Sku, int QtyNeeded, int QtyOnHand)> lines)
        {
            var result = new List<(string Sku, int Allocated)>();

            foreach (var line in lines)
            {
                var allocated = Math.Min(
                    line.QtyNeeded,
                    line.QtyOnHand);

                result.Add((line.Sku, allocated));
            }

            return result;
        }
    }
}
