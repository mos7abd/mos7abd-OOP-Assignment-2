using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._10_AppointmentDesk
{

    /// <summary>
    /// Formats appointment reminder messages for SMS communication.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when appointment reminder messaging or SMS wording changes.
    /// </remarks>
    public sealed class SmsReminderFormatter
    {
        public string Format(
            DateTimeOffset slot,
            string clinicPhone)
        {
            return $"Reminder: appointment {slot:MMM dd HH:mm}. " +
                   $"Call {clinicPhone} to reschedule.";
        }
    }
}
