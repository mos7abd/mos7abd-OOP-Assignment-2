using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._10_AppointmentDesk
{

    /// <summary>
    /// Formats appointment details as an ICS calendar event.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when calendar interoperability or ICS serialization rules change.
    /// </remarks>
    public sealed class IcsCalendarFormatter
    {
        public string Format(
            DateTimeOffset slot,
            int slotMinutes,
            string patientName,
            string clinician)
        {
            var uid = Guid.NewGuid();
            var end = slot.AddMinutes(slotMinutes);

            return "BEGIN:VCALENDAR\n" +
                   "VERSION:2.0\n" +
                   "BEGIN:VEVENT\n" +
                   $"UID:{uid}\n" +
                   $"DTSTART:{slot:yyyyMMdd'T'HHmmss'Z'}\n" +
                   $"DTEND:{end:yyyyMMdd'T'HHmmss'Z'}\n" +
                   $"SUMMARY:Visit {patientName} / {clinician}\n" +
                   "END:VEVENT\n" +
                   "END:VCALENDAR\n";
        }
    }
}
