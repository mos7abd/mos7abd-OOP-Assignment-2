using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._1_WardBoard
{

    /// <summary>
    /// Formats patient and acuity information into a nurse handoff note.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when the format or content of the nurse handoff note changes.
    /// </remarks>
    public sealed class HandoffNoteFormatter
    {
        public string Format(int bed, string patient, int acuity)
        {
            var tone = acuity >= 8
                ? "ESCALATE"
                : acuity >= 4
                    ? "WATCH"
                    : "STABLE";

            return $"[HANDOFF {DateTime.UtcNow:yyyy-MM-dd}] Bed {bed} · {patient} · acuity={acuity} · {tone}";
        }
    }
}
