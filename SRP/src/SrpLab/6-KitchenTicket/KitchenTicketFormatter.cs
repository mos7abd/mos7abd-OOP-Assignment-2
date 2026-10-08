using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._6_KitchenTicket
{

    /// <summary>
    /// Formats kitchen tickets for thermal printing and determines the expo lane.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when the kitchen ticket layout or expo routing format changes.
    /// </remarks>
    public sealed class KitchenTicketFormatter
    {
        public string RenderThermalTicket(
            int orderNumber,
            IEnumerable<(string Item, int PrepMinutes)> items,
            int estimatedReadyMinutes,
            IReadOnlyList<string> allergens)
        {
            var width = 32;
            var line = new string('=', width);

            var body = string.Join(
                '\n',
                items.Select(i =>
                    $"* {i.Item.ToUpperInvariant()} ({i.PrepMinutes}m)"));

            var allergyLine = allergens.Count == 0
                ? "ALLERGENS: none"
                : "ALLERGENS: " + string.Join(",", allergens);

            return $"{line}\n" +
                   $"ORDER #{orderNumber}\n" +
                   $"ETA {estimatedReadyMinutes} MIN\n" +
                   $"{body}\n" +
                   $"{allergyLine}\n" +
                   $"{line}\n";
        }

        public string GetExpoLane(
            IReadOnlyList<string> allergens,
            int estimatedReadyMinutes)
        {
            return allergens.Count > 0
                ? "LANE-ALLERGY"
                : estimatedReadyMinutes > 20
                    ? "LANE-SLOW"
                    : "LANE-FAST";
        }
    }
}
