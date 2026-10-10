using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem
{


    // Represents a librarian in the library.
    public class Librarian : Staff
    {
        // Creates a librarian with the required information.
        public Librarian(
            int personId,
            string fullName,
            string phone,
            DateTime hireDate,
            decimal monthlySalary)
            : base(
                personId,
                fullName,
                phone,
                hireDate,
                monthlySalary)
        {
        }

        // Returns a borrowed loan.
        public void ProcessReturn(Loan loan, DateTime returnDate)
        {
            // Ensures that the loan exists.
            ArgumentNullException.ThrowIfNull(loan);

            // Returns the loan and updates its status.
            loan.Return(returnDate);
        }

        // Marks a borrowed loan as lost.
        public void MarkItemAsLost(Loan loan)
        {
            // Ensures that the loan exists.
            ArgumentNullException.ThrowIfNull(loan);

            // Marks the loan as lost.
            loan.MarkAsLost();
        }
    }


}
