using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._5_CourseEnrollmentDesk
{

    /// <summary>
    /// Formats the student welcome packet as Markdown.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when the welcome packet content or format changes.
    /// </remarks>
    public sealed class WelcomePacketFormatter
    {
        public string Format(
            string courseCode,
            string studentName,
            string status)
        {
            return $"# Welcome to {courseCode}\n" +
                   $"Hi {studentName},\n" +
                   $"Your status: **{status}**.\n" +
                   $"Bring a laptop. Discord onboarding link: " +
                   $"https://example.invalid/{courseCode.ToLowerInvariant()}\n";
        }
    }
}
