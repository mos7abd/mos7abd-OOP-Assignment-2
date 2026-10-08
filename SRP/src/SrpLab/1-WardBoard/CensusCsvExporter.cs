using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._1_WardBoard
{
    /// <summary>
    /// Formats ward census data as CSV.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when the format for exporting ward census data to CSV changes.
    /// </remarks>
    public sealed class CensusCsvExporter
    {
        public string Export(IEnumerable<(int Bed, string Patient, int Acuity)> entries)
        {
            var lines = new List<string>
        {
            "bed,patient,acuity"
        };

            foreach (var entry in entries.OrderBy(x => x.Bed))
            {
                lines.Add($"{entry.Bed},{entry.Patient},{entry.Acuity}");
            }

            return string.Join('\n', lines);
        }
    }
}
