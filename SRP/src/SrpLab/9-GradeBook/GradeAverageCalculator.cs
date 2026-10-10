using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._9_GradeBook
{

    /// <summary>
    /// Calculates the average score for a student's recorded grades.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when grade aggregation or average calculation rules change.
    /// </remarks>
    public sealed class GradeAverageCalculator
    {
        public decimal Calculate(IEnumerable<decimal> scores)
        {
            var scoreList = scores.ToList();

            if (scoreList.Count == 0)
                return 0m;

            return Math.Round(scoreList.Average(), 2);
        }
    }
}
