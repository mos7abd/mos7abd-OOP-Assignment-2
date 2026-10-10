using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem
{

    // Represents a DVD in the library.
    public class DVD : LibraryItem
    {
        // Creates a DVD with a 7-day loan period and double late fee.
        public DVD(
            int catalogNumber,
            string title,
            decimal baseLateFee)
            : base(
                catalogNumber,
                title,
                baseLateFee,
                7,
                2m)
        {
        }
    }

}
