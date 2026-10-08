using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._1_WardBoard
{
    /// <summary>
    /// Records pager alerts when a patient's acuity requires escalation.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when the rules for recording pager alerts change.
    /// </remarks>
    public sealed class PagerAlertRecorder
    {
        private readonly List<string> _alerts = new();

        public void Record(int bed, int acuity)
        {
            if (acuity >= 8)
            {
                _alerts.Add($"CODE-YELLOW bed={bed} at {DateTime.UtcNow:HH:mm}");
            }
        }

        public IReadOnlyList<string> Drain()
        {
            var copy = _alerts.ToList();
            _alerts.Clear();
            return copy;
        }
    }
}
