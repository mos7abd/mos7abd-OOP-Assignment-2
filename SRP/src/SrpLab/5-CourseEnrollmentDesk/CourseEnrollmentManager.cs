using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._5_CourseEnrollmentDesk
{

    /// <summary>
    /// Manages student enrollment, seat capacity, and the course waitlist.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when course enrollment, seat, or waitlist rules change.
    /// </remarks>
    public sealed class CourseEnrollmentManager
    {
        private readonly HashSet<string> _seated =
            new(StringComparer.OrdinalIgnoreCase);

        private readonly List<string> _waitlist = new();

        private readonly int _capacity;

        public CourseEnrollmentManager(int capacity)
        {
            _capacity = capacity;
        }

        public string Register(string studentEmail)
        {
            if (string.IsNullOrWhiteSpace(studentEmail))
                throw new ArgumentException("email");

            var email = studentEmail.Trim();

            if (_seated.Contains(email) || _waitlist.Contains(email))
                return "ALREADY_REGISTERED";

            if (_seated.Count < _capacity)
            {
                _seated.Add(email);
                return "SEATED";
            }

            _waitlist.Add(email);
            return $"WAITLIST:{_waitlist.Count}";
        }

        public int WaitlistPosition(string studentEmail)
        {
            var index = _waitlist.FindIndex(
                x => x.Equals(
                    studentEmail,
                    StringComparison.OrdinalIgnoreCase));

            return index < 0 ? -1 : index + 1;
        }

        public void PromoteFromWaitlist(int seats)
        {
            while (seats > 0 &&
                   _waitlist.Count > 0 &&
                   _seated.Count < _capacity)
            {
                var next = _waitlist[0];

                _waitlist.RemoveAt(0);
                _seated.Add(next);

                seats--;
            }
        }

        public bool IsSeated(string studentEmail)
        {
            return _seated.Contains(studentEmail);
        }
    }
}
