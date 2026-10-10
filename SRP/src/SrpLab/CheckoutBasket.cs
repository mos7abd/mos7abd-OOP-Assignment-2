using SrpLab._2_CheckoutBasket;

namespace SrpLab;

/// <summary>
/// Manages basket contents and coordinates basket-related operations.
/// </summary>
/// <remarks>
/// Reason to change: Changes when the way basket contents are managed changes.
/// </remarks>
public sealed class CheckoutBasket
{
    private readonly List<(string Sku, decimal Price, int Qty)> _lines = new();

    private string? _couponRaw;
    private bool _giftWrap;

    private readonly BasketPricingCalculator _pricingCalculator;
    private readonly CouponDiscountCalculator _couponDiscountCalculator;
    private readonly GiftMessageFormatter _giftMessageFormatter;
    private readonly PaymentAuthorizationStub _paymentAuthorizationStub;

    public CheckoutBasket()
    {
        _pricingCalculator = new BasketPricingCalculator();
        _couponDiscountCalculator = new CouponDiscountCalculator();
        _giftMessageFormatter = new GiftMessageFormatter();
        _paymentAuthorizationStub = new PaymentAuthorizationStub();
    }

    public void AddLine(string sku, decimal price, int qty)
    {
        if (qty <= 0)
            throw new ArgumentOutOfRangeException(nameof(qty));

        _lines.Add((sku, price, qty));
    }

    public void ApplyCouponText(string? couponText)
    {
        _couponRaw = couponText;
    }

    public void EnableGiftWrap()
    {
        _giftWrap = true;
    }

    public decimal SubTotal()
    {
        return _pricingCalculator.CalculateSubTotal(_lines);
    }

    public decimal DiscountAmount()
    {
        return _couponDiscountCalculator.Calculate(
            _couponRaw,
            SubTotal());
    }

    public decimal GrandTotal()
    {
        return _pricingCalculator.CalculateGrandTotal(
            _lines,
            DiscountAmount(),
            _giftWrap);
    }

    public string GiftMessageCard(string fromName)
    {
        var items = _lines.Select(line => line.Sku);

        return _giftMessageFormatter.Format(
            fromName,
            items,
            GrandTotal());
    }

    public string AuthorizePaymentStub(string cardLast4)
    {
        return _paymentAuthorizationStub.Authorize(
            GrandTotal(),
            cardLast4,
            _lines.Count);
    }
}
