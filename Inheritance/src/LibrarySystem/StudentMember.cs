using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem
{


    // Represents a student member of the library.
    public class StudentMember : Member
    {
        // Creates a student member with a maximum of 3 active loans
        // and no late-fee discount.
        public StudentMember(
            int personId,
            string fullName,
            string phone)
            : base(
                personId,
                fullName,
                phone,
                3,
                0m)
        {
        }
    }

}
