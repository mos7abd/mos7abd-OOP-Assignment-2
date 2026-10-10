using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._9_GradeBook
{

    /// <summary>
    /// Applies the academic policy that maps an average score to a letter grade.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when academic letter-grade band policies change.
    /// </remarks>
    public sealed class LetterGradePolicy
    {
        public string GetLetter(decimal average)
        {
            if (average >= 90)
                return "A";

            if (average >= 80)
                return "B";

            if (average >= 70)
                return "C";

            if (average >= 60)
                return "D";

            return "F";
        }
    }
}
