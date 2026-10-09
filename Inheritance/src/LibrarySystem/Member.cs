using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem
{

    // Represents a library member.
    public class Member : Person
    {
        // Stores the member's loans privately.
        private readonly List<Loan> _loans = new();

        // Exposes loans without allowing outside code to add or remove them.
        public IReadOnlyList<Loan> Loans => _loans.AsReadOnly();

        // Gets the maximum number of active loans.
        public int MaxActiveLoans { get; }

        // Gets the member's late fee discount percentage.
        public decimal LateFeeDiscountPercentage { get; }

        // Creates a member and passes personal data to Person.
        // Protected prevents creating Member directly.
        protected Member(
            int personId,
            string fullName,
            string phone,
            int maxActiveLoans,
            decimal lateFeeDiscountPercentage)
            : base(personId, fullName, phone)
        {
            // Validates the maximum number of active loans.
            if (maxActiveLoans <= 0)
                throw new ArgumentException(
                    "Maximum active loans must be greater than zero.",
                    nameof(maxActiveLoans));

            // Validates the discount percentage.
            if (lateFeeDiscountPercentage < 0 ||
                lateFeeDiscountPercentage > 100)
                throw new ArgumentException(
                    "Discount percentage must be between 0 and 100.",
                    nameof(lateFeeDiscountPercentage));

            // Stores the validated member settings.
            MaxActiveLoans = maxActiveLoans;
            LateFeeDiscountPercentage = lateFeeDiscountPercentage;
        }

        // Borrows an item and records the loan.
        public Loan Borrow(
            LibraryItem item,
            int loanId,
            DateTime borrowDate)
        {
            // Ensures an item was provided.
            ArgumentNullException.ThrowIfNull(item);

            // Counts the member's active loans.
            int activeLoans = 0;

            foreach (Loan loan in _loans)
            {
                if (loan.Status == LoanStatus.Borrowed)
                    activeLoans++;
            }

            // Rejects borrowing when the member has reached the limit.
            if (activeLoans >= MaxActiveLoans)
                throw new InvalidOperationException(
                    "The member has reached the maximum loan limit.");

            // Rejects items withdrawn from the library.
            if (item.IsWithdrawn)
                throw new InvalidOperationException(
                    "A withdrawn item cannot be borrowed.");

            // Rejects items that are already on loan.
            if (item.IsOnLoan)
                throw new InvalidOperationException(
                    "This item is already on loan.");

            // Creates a loan using the member and the actual item.
            Loan newLoan = new Loan(
                loanId,
                this,
                item,
                borrowDate);

            // Marks the item as borrowed.
            item.IsOnLoan = true;

            // Adds the loan to the private collection.
            _loans.Add(newLoan);

            // Returns the created loan.
            return newLoan;
        }

        // Adds reading points when a loan is returned.
        public virtual void AddReadingPoints()
        {
            // Regular members do not receive reading points.
        }

    }

}
