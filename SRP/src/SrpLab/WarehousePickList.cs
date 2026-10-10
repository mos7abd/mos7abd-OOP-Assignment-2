using SrpLab._8_WarehousePickList;

namespace SrpLab;

/// <summary>
/// Manages warehouse picking needs and coordinates specialized pick-list operations.
/// </summary>
/// <remarks>
/// Reason to change: Changes when the way warehouse picking needs are stored or coordinated changes.
/// </remarks>
public sealed class WarehousePickList
{
    private readonly List<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> _lines = new();

    private readonly StockAllocationCalculator _allocationCalculator;
    private readonly WarehouseWalkingOrderCalculator _walkingOrderCalculator;
    private readonly PickerInstructionFormatter _pickerInstructionFormatter;
    private readonly WmsXmlFormatter _wmsXmlFormatter;

    public WarehousePickList()
    {
        _allocationCalculator = new StockAllocationCalculator();
        _walkingOrderCalculator = new WarehouseWalkingOrderCalculator();
        _pickerInstructionFormatter = new PickerInstructionFormatter();
        _wmsXmlFormatter = new WmsXmlFormatter();
    }

    public void AddNeed(
        string sku,
        string aisle,
        int bin,
        int qtyNeeded,
        int qtyOnHand)
    {
        _lines.Add((
            sku,
            aisle,
            bin,
            qtyNeeded,
            qtyOnHand));
    }

    public IReadOnlyList<(string Sku, int Allocated)> Allocate()
    {
        return _allocationCalculator.Calculate(
            _lines.Select(line => (
                line.Sku,
                line.QtyNeeded,
                line.QtyOnHand)));
    }

    public IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> WalkingOrder()
    {
        return _walkingOrderCalculator.Calculate(_lines);
    }

    public string PickerScript()
    {
        var walkingOrder = WalkingOrder();
        var allocations = Allocate();

        var requirements = _lines.Select(line => (
            line.Sku,
            line.QtyNeeded));

        return _pickerInstructionFormatter.Format(
            walkingOrder,
            allocations,
            requirements);
    }

    public string WmsXmlBatch(string batchId)
    {
        var allocations = Allocate();

        return _wmsXmlFormatter.Format(
            batchId,
            allocations);
    }
}