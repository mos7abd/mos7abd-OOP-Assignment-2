using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem
{

    // Represents a premium member of the library.
    public class PremiumMember : Member
    {
        // Stores the member's reading points privately.
        private int _readingPoints;

        // Gets the member's reading points without allowing direct changes.
        public int ReadingPoints => _readingPoints;

        // Creates a premium member with a maximum of 10 active loans.
        // The discount is set at registration and cannot be changed later.
        public PremiumMember(
            int personId,
            string fullName,
            string phone,
            decimal discount)
            : base(
                personId,
                fullName,
                phone,
                10,
                discount)
        {
            // Starts the member with zero reading points.
            _readingPoints = 0;
        }

        // Adds 5 reading points after a loan is returned.
        public override void AddReadingPoints()
        {
            // Adds 5 points to the member's total.
            _readingPoints += 5;
        }
    }

}
