using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem
{

    // Represents a library employee.
    public class Staff : Person
    {
        // Gets the employee's hire date.
        public DateTime HireDate { get; }

        // Gets the employee's monthly salary.
        // Only this class can change the salary.
        public decimal MonthlySalary { get; private set; }

        // Creates a staff member with the required information.
        // Protected prevents direct creation of Staff objects.
        protected Staff(
            int personId,
            string fullName,
            string phone,
            DateTime hireDate,
            decimal monthlySalary)
            : base(personId, fullName, phone)
        {
            // Validates the hire date.
            if (hireDate == default)
                throw new ArgumentException(
                    "Hire date must be a valid date.",
                    nameof(hireDate));

            // Validates the monthly salary.
            if (monthlySalary <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(monthlySalary),
                    "Monthly salary must be greater than zero.");

            // Stores the validated values.
            HireDate = hireDate;
            MonthlySalary = monthlySalary;
        }


        // Increases the monthly salary by a percentage.
        public void GiveRaise(decimal percentage)
        {
            // Rejects a percentage that is zero or negative.
            if (percentage <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(percentage),
                    "Raise percentage must be greater than zero.");

            // Calculates the salary increase.
            decimal increase = MonthlySalary * percentage / 100m;

            // Updates the monthly salary.
            MonthlySalary += increase;
        }


        // Calculates the employee's monthly pay.
        public virtual decimal CalculateMonthlyPay()
        {
            return MonthlySalary;
        }
    }

}
