using System;
using System.Collections.Generic;
using System.Text;


namespace LibrarySystem;

// Represents the common data and behavior of library items.
public class LibraryItem
{
    // Gets the item's catalog number.
    public int CatalogNumber { get; }

    // Gets the item's title.
    public string Title { get; }

    // Gets the base late fee.
    public decimal BaseLateFee { get; private set; }

    // Gets the number of days allowed for borrowing.
    public int LoanPeriodDays { get; }

    // Gets the multiplier used to calculate the daily late fee.
    public decimal LateFeeMultiplier { get; }

    // Indicates whether the item has been withdrawn.
    public bool IsWithdrawn { get; private set; }

    // Indicates whether the item is currently on loan.
    public bool IsOnLoan { get; internal set; }

    // Creates an item and validates its initial values.
    protected LibraryItem(
        int catalogNumber,
        string title,
        decimal baseLateFee,
        int loanPeriodDays,
        decimal lateFeeMultiplier)
    {
        // Validates the catalog number.
        if (catalogNumber <= 0)
            throw new ArgumentException(
                "Catalog number must be greater than zero.",
                nameof(catalogNumber));

        // Validates the title.
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException(
                "Title cannot be empty.",
                nameof(title));

        // Validates the base late fee.
        if (baseLateFee <= 0)
            throw new ArgumentException(
                "Base late fee must be greater than zero.",
                nameof(baseLateFee));

        // Validates the borrowing period.
        if (loanPeriodDays <= 0)
            throw new ArgumentException(
                "Loan period must be greater than zero.",
                nameof(loanPeriodDays));

        // Validates the late fee multiplier.
        if (lateFeeMultiplier <= 0)
            throw new ArgumentException(
                "Late fee multiplier must be greater than zero.",
                nameof(lateFeeMultiplier));

        // Stores the validated values.
        CatalogNumber = catalogNumber;
        Title = title;
        BaseLateFee = baseLateFee;
        LoanPeriodDays = loanPeriodDays;
        LateFeeMultiplier = lateFeeMultiplier;
    }

    // Changes the base late fee.
    public void ChangeBaseLateFee(decimal newBaseLateFee)
    {
        // Rejects zero or negative fees.
        if (newBaseLateFee <= 0)
            throw new ArgumentException(
                "Base late fee must be greater than zero.",
                nameof(newBaseLateFee));

        // Updates the base late fee.
        BaseLateFee = newBaseLateFee;
    }

    // Withdraws an item from the library.
    public void Withdraw()
    {
        // Prevents withdrawing an item that is on loan.
        if (IsOnLoan)
            throw new InvalidOperationException(
                "Cannot withdraw an item while it is on loan.");

        // Marks the item as withdrawn.
        IsWithdrawn = true;
    }

    // Restores a withdrawn item.
    public void Restore()
    {
        // Makes the item available for borrowing again.
        IsWithdrawn = false;
    }

    // Calculates the daily late fee for any item type.
    public decimal GetDailyLateFee()
    {
        // Applies the item's late fee multiplier.
        return BaseLateFee * LateFeeMultiplier;
    }
}

