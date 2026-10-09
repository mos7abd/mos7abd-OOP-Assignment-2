using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem
{


    // Represents the head librarian.
    public class HeadLibrarian : Staff
    {
        // Stores the fixed responsibility allowance.
        private const decimal ResponsibilityAllowance = 400m;

        // Creates a head librarian.
        public HeadLibrarian(
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

        // Calculates the monthly pay including the allowance.
        public override decimal CalculateMonthlyPay()
        {
            return MonthlySalary + ResponsibilityAllowance;
        }

        // Changes the late fee of a library item.
        public void ChangeLateFee(
            LibraryItem item,
            decimal newBaseLateFee)
        {
            ArgumentNullException.ThrowIfNull(item);

            item.ChangeBaseLateFee(newBaseLateFee);
        }

        // Withdraws an item from circulation.
        public void WithdrawItem(LibraryItem item)
        {
            ArgumentNullException.ThrowIfNull(item);

            item.Withdraw();
        }

        // Restores an item to circulation.
        public void RestoreItem(LibraryItem item)
        {
            ArgumentNullException.ThrowIfNull(item);

            item.Restore();
        }
    }


}
