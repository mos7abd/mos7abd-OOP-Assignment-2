using SrpLab._7_SubscriptionBilling;

namespace SrpLab;

/// <summary>
/// Manages subscription billing data and coordinates billing operations.
/// </summary>
/// <remarks>
/// Reason to change: Changes when the way subscription billing data and operations are coordinated changes.
/// </remarks>
public sealed class SubscriptionBilling
{
    public string CustomerId { get; }
    public decimal MonthlyPrice { get; }
    public DateOnly PeriodStart { get; }
    public DateOnly PeriodEnd { get; }

    public int FailedPayments =>
        _failedPaymentTracker.FailedPayments;

    private readonly SubscriptionProrationCalculator _prorationCalculator;
    private readonly InvoiceNumberGenerator _invoiceNumberGenerator;
    private readonly FailedPaymentTracker _failedPaymentTracker;
    private readonly DunningEmailFormatter _dunningEmailFormatter;
    private readonly LedgerJournalFormatter _ledgerJournalFormatter;

    public SubscriptionBilling(
        string customerId,
        decimal monthlyPrice,
        DateOnly periodStart,
        DateOnly periodEnd)
    {
        CustomerId = customerId;
        MonthlyPrice = monthlyPrice;
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;

        _prorationCalculator = new SubscriptionProrationCalculator();
        _invoiceNumberGenerator = new InvoiceNumberGenerator();
        _failedPaymentTracker = new FailedPaymentTracker();
        _dunningEmailFormatter = new DunningEmailFormatter();
        _ledgerJournalFormatter = new LedgerJournalFormatter();
    }

    public decimal Prorate(DateOnly activeFrom)
    {
        return _prorationCalculator.Calculate(
            MonthlyPrice,
            PeriodStart,
            PeriodEnd,
            activeFrom);
    }

    public string NextInvoiceNumber()
    {
        return _invoiceNumberGenerator.Generate(PeriodStart);
    }

    public void RegisterFailedPayment()
    {
        _failedPaymentTracker.RegisterFailedPayment();
    }

    public string DunningEmail(
        string customerName,
        DateOnly asOf)
    {
        var amount = Prorate(PeriodStart);

        // Preserve the original side effect:
        // composing the email generates a new invoice number.
        var invoice = NextInvoiceNumber();

        return _dunningEmailFormatter.Format(
            customerName,
            amount,
            invoice,
            FailedPayments,
            asOf);
    }

    public string LedgerJournalLine(DateOnly activeFrom)
    {
        var invoiceNumber = NextInvoiceNumber();
        var amount = Prorate(activeFrom);

        return _ledgerJournalFormatter.Format(
            CustomerId,
            invoiceNumber,
            amount);
    }
}
