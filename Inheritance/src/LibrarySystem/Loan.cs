using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem
{


    // Represents one borrowing operation in the library.
    public class Loan
    {
        // Gets the loan identifier.
        public int LoanId { get; }

        // Gets the date when the item was borrowed.
        public DateTime BorrowDate { get; }

        // Gets the member who borrowed the item.
        public Member Member { get; }

        // Gets the actual library item being borrowed.
        public LibraryItem Item { get; }

        // Gets the current loan status.
        // Only this class can change the status.
        public LoanStatus Status { get; private set; }

        // Gets the return date, or null if the item has not been returned.
        public DateTime? ReturnDate { get; private set; }

        // Calculates the due date using the item's loan period.
        public DateTime DueDate =>
            BorrowDate.AddDays(Item.LoanPeriodDays);

        // Gets the calculated late fee.
        public decimal LateFee => CalculateLateFee();

        // Creates a loan for a member and a library item.
        public Loan(
            int loanId,
            Member member,
            LibraryItem item,
            DateTime borrowDate)
        {
            // Validates the loan identifier.
            if (loanId <= 0)
                throw new ArgumentException(
                    "Loan ID must be greater than zero.",
                    nameof(loanId));

            // Ensures a member was provided.
            ArgumentNullException.ThrowIfNull(member);

            // Ensures a library item was provided.
            ArgumentNullException.ThrowIfNull(item);

            // Validates the borrow date.
            if (borrowDate == default)
                throw new ArgumentException(
                    "Borrow date must be a valid date.",
                    nameof(borrowDate));

            // Stores the validated values.
            LoanId = loanId;
            Member = member;
            Item = item;
            BorrowDate = borrowDate;

            // Starts the loan in the borrowed state.
            Status = LoanStatus.Borrowed;
        }

        // Returns the borrowed item on the specified date.
        public void Return(DateTime returnDate)
        {
            // Only a borrowed loan can be returned.
            if (Status != LoanStatus.Borrowed)
                throw new InvalidOperationException(
                    "Only borrowed loans can be returned.");

            // Prevents a return date before the borrow date.
            if (returnDate < BorrowDate)
                throw new ArgumentException(
                    "Return date cannot be earlier than borrow date.",
                    nameof(returnDate));

            // Records the return date.
            ReturnDate = returnDate;

            // Changes the loan status to returned.
            Status = LoanStatus.Returned;

            // Makes the library item available again.
            Item.IsOnLoan = false;

            // Gives 5 reading points to a premium member.
            Member.AddReadingPoints();
        }


        // Marks the borrowed item as lost.
        public void MarkAsLost()
        {
            // Only a borrowed loan can be marked as lost.
            if (Status != LoanStatus.Borrowed)
                throw new InvalidOperationException(
                    "Only borrowed loans can be marked as lost.");

            // Changes the loan status to lost.
            Status = LoanStatus.Lost;
        }


        // Calculates the late fee for this loan.
        private decimal CalculateLateFee()
        {
            // No late fee is charged before the item is returned.
            if (ReturnDate is null)
                return 0m;

            // No late fee is charged if the item was returned on time.
            if (ReturnDate.Value.Date <= DueDate.Date)
                return 0m;

            // Calculates the number of late days.
            int lateDays =
                (ReturnDate.Value.Date - DueDate.Date).Days;

            // Gets the daily late fee for this item.
            decimal dailyLateFee = Item.GetDailyLateFee();

            // Calculates the fee before applying the discount.
            decimal totalFee = lateDays * dailyLateFee;

            // Calculates the member's discount rate.
            decimal discount =
                Member.LateFeeDiscountPercentage / 100m;

            // Returns the fee after applying the discount.
            return totalFee * (1m - discount);
        }
    }

}
