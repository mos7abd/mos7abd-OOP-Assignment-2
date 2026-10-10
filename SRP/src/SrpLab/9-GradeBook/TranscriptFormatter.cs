using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._9_GradeBook
{

    /// <summary>
    /// Formats student academic results as a plain-text transcript.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when the plain-text transcript format changes.
    /// </remarks>
    public sealed class TranscriptFormatter
    {
        public string Format(
            string studentId,
            string fullName,
            decimal average,
            string letter,
            bool honor)
        {
            return $"TRANSCRIPT\n" +
                   $"Student: {fullName} ({studentId})\n" +
                   $"Average: {average}\n" +
                   $"Letter: {letter}\n" +
                   $"Honor: {honor}\n";
        }
    }
}
