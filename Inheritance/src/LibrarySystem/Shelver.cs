using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem
{


    // Represents a staff member responsible for shelving library items.
    public class Shelver : Staff
    {
        // Gets the section assigned to the shelver.
        // Only this class can change the section.
        public string Section { get; private set; }

        // Creates a shelver with the required information.
        public Shelver(
            int personId,
            string fullName,
            string phone,
            DateTime hireDate,
            decimal monthlySalary,
            string section)
            : base(
                personId,
                fullName,
                phone,
                hireDate,
                monthlySalary)
        {
            // Validates the assigned section.
            if (string.IsNullOrWhiteSpace(section))
                throw new ArgumentException(
                    "Section cannot be empty.",
                    nameof(section));

            // Stores the assigned section.
            Section = section;
        }

        // Reassigns the shelver to another section.
        public void ReassignSection(string newSection)
        {
            // Validates the new section.
            if (string.IsNullOrWhiteSpace(newSection))
                throw new ArgumentException(
                    "Section cannot be empty.",
                    nameof(newSection));

            // Updates the assigned section.
            Section = newSection;
        }
    }

}
