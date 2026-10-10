using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem
{

    // Represents the possible states of a loan.
    public enum LoanStatus
    {
        // The item is currently borrowed.
        Borrowed,

        // The item has been returned.
        Returned,

        // The item has been marked as lost.
        Lost
    }

}
