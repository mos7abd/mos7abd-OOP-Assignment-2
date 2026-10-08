using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._9_GradeBook
{

    /// <summary>
    /// Exports student academic results in CSV format.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when the CSV export format changes.
    /// </remarks>
    public sealed class GradeBookCsvExporter
    {
        public string Export(
            IEnumerable<(string StudentId, decimal Average, string Letter, bool Honor)> students)
        {
            var rows = new List<string>
        {
            "studentId,average,letter,honor"
        };

            foreach (var student in students.OrderBy(x => x.StudentId))
            {
                rows.Add(
                    $"{student.StudentId}," +
                    $"{student.Average}," +
                    $"{student.Letter}," +
                    $"{(student.Honor ? 1 : 0)}");
            }

            return string.Join('\n', rows);
        }
    }
}
