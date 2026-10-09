using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem
{

    // Represents a magazine in the library.
    public class Magazine : LibraryItem
    {
        // Creates a magazine with a 3-day loan period
        // and half the base late fee.
        public Magazine(
            int catalogNumber,
            string title,
            decimal baseLateFee)
            : base(
                catalogNumber,
                title,
                baseLateFee,
                3,
                0.5m)
        {
        }
    }

}
