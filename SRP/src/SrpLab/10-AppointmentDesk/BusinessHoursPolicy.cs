using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._10_AppointmentDesk
{

    /// <summary>
    /// Determines whether appointment slots fall within the configured business hours.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when business-hour or clinic scheduling rules change.
    /// </remarks>
    public sealed class BusinessHoursPolicy
    {
        public bool IsWithin(
            DateTimeOffset when,
            TimeOnly open,
            TimeOnly close,
            int slotMinutes)
        {
            if (when.DayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday)
                return false;

            var time = TimeOnly.FromDateTime(when.DateTime);

            return time >= open &&
                   time.AddMinutes(slotMinutes) <= close;
        }
    }
}
