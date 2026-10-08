using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._8_WarehousePickList
{

    /// <summary>
    /// Formats allocated warehouse items as a WMS XML batch.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when the WMS integration XML contract changes.
    /// </remarks>
    public sealed class WmsXmlFormatter
    {
        public string Format(
            string batchId,
            IEnumerable<(string Sku, int Allocated)> allocations)
        {
            var parts = allocations
                .Select(a =>
                    $"<line sku=\"{a.Sku}\" qty=\"{a.Allocated}\" />");

            return $"<batch id=\"{batchId}\">{string.Join("", parts)}</batch>";
        }
    }
}
