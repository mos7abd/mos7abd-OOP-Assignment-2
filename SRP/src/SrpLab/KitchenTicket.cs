using SrpLab._6_KitchenTicket;

namespace SrpLab;

/// <summary>
/// Manages kitchen order items and coordinates specialized ticket operations.
/// </summary>
/// <remarks>
/// Reason to change: Changes when the way kitchen order items are managed changes.
/// </remarks>
public sealed class KitchenTicket
{
    private readonly List<(string Item, List<string> Ingredients, int PrepMinutes)> _items = new();

    private readonly KitchenAllergenDetector _allergenDetector;
    private readonly KitchenEtaCalculator _etaCalculator;
    private readonly KitchenTicketFormatter _ticketFormatter;

    public KitchenTicket()
    {
        _allergenDetector = new KitchenAllergenDetector();
        _etaCalculator = new KitchenEtaCalculator();
        _ticketFormatter = new KitchenTicketFormatter();
    }

    public void AddItem(
        string item,
        IEnumerable<string> ingredients,
        int prepMinutes)
    {
        _items.Add((
            item,
            ingredients
                .Select(i => i.Trim().ToLowerInvariant())
                .ToList(),
            prepMinutes));
    }

    public IReadOnlyList<string> DetectAllergens()
    {
        return _allergenDetector.Detect(
            _items.Select(i => i.Ingredients));
    }

    public int EstimatedReadyMinutes(int openStations)
    {
        return _etaCalculator.Calculate(
            _items.Select(i => i.PrepMinutes),
            openStations,
            DetectAllergens().Count > 0);
    }

    public string RenderThermalTicket(int orderNumber)
    {
        var allergens = DetectAllergens();
        var estimatedReadyMinutes = EstimatedReadyMinutes(2);

        return _ticketFormatter.RenderThermalTicket(
            orderNumber,
            _items.Select(i => (i.Item, i.PrepMinutes)),
            estimatedReadyMinutes,
            allergens);
    }

    public string ExpoLaneHint()
    {
        var allergens = DetectAllergens();
        var estimatedReadyMinutes = EstimatedReadyMinutes(2);

        return _ticketFormatter.GetExpoLane(
            allergens,
            estimatedReadyMinutes);
    }
}