using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._8_WarehousePickList
{

    /// <summary>
    /// Determines the order in which warehouse locations should be visited.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when warehouse walking or path-ordering rules change.
    /// </remarks>
    public sealed class WarehouseWalkingOrderCalculator
    {
        public IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> Calculate(
            IEnumerable<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> lines)
        {
            return lines
                .OrderBy(l => l.Aisle)
                .ThenBy(l => l.Bin)
                .Select(l => (
                    l.Aisle,
                    l.Bin,
                    l.Sku,
                    Math.Min(l.QtyNeeded, l.QtyOnHand)))
                .Where(x => x.Item4 > 0)
                .ToList();
        }
    }
}
