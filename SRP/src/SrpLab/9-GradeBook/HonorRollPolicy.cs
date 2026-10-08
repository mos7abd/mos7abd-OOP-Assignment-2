using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._9_GradeBook
{

    /// <summary>
    /// Determines whether a student meets the academic honor-roll requirements.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when academic honor-roll eligibility rules change.
    /// </remarks>
    public sealed class HonorRollPolicy
    {
        public bool MeetsHonorRoll(decimal average, string letter)
        {
            return average >= 85 &&
                   letter is "A" or "B";
        }
    }
}
