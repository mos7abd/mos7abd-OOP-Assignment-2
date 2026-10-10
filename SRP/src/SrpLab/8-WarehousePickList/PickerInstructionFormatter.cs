using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._8_WarehousePickList
{

    /// <summary>
    /// Formats warehouse picking instructions and shortage messages for pickers.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when picker instruction wording or shortage messaging changes.
    /// </remarks>
    public sealed class PickerInstructionFormatter
    {
        public string Format(
            IEnumerable<(string Aisle, int Bin, string Sku, int Qty)> walkingOrder,
            IEnumerable<(string Sku, int Allocated)> allocations,
            IEnumerable<(string Sku, int QtyNeeded)> requirements)
        {
            var steps = walkingOrder
                .Select((step, index) =>
                    $"{index + 1}. Go aisle {step.Aisle} bin {step.Bin}: " +
                    $"pick {step.Qty} × {step.Sku}");

            var requiredQuantities = requirements.ToList();

            var shortfalls = allocations.Where(allocation =>
            {
                var requirement = requiredQuantities
                    .First(line => line.Sku == allocation.Sku);

                return allocation.Allocated < requirement.QtyNeeded;
            });

            var warning = shortfalls.Any()
                ? "SHORTAGES: " +
                  string.Join(", ", shortfalls.Select(s => s.Sku))
                : "SHORTAGES: none";

            return string.Join('\n', steps) + "\n" + warning;
        }
    }
}
