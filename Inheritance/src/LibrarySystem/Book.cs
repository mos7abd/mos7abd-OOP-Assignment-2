using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem
{

    // Represents a book in the library.
    public class Book : LibraryItem
    {
        // Creates a book with a 21-day loan period and a normal late fee.
        public Book(
            int catalogNumber,
            string title,
            decimal baseLateFee)
            : base(
                catalogNumber,
                title,
                baseLateFee,
                21,
                1m)
        {
        }
    }

}
