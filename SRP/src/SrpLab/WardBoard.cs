using SrpLab._1_WardBoard;

namespace SrpLab;

/// <summary>
/// Manages bed assignments and coordinates ward-related operations.
/// </summary>
/// <remarks>
/// Reason to change: Changes when the way ward bed and patient data is managed changes.
/// </remarks>
public sealed class WardBoard
{
    private readonly Dictionary<int, string> _bedPatient = new();
    private readonly Dictionary<int, int> _vitalsScore = new();

    private readonly AcuityScorer _acuityScorer;
    private readonly PagerAlertRecorder _pagerAlertRecorder;
    private readonly HandoffNoteFormatter _handoffNoteFormatter;
    private readonly CensusCsvExporter _censusCsvExporter;

    public WardBoard()
    {
        _acuityScorer = new AcuityScorer();
        _pagerAlertRecorder = new PagerAlertRecorder();
        _handoffNoteFormatter = new HandoffNoteFormatter();
        _censusCsvExporter = new CensusCsvExporter();
    }

    public void AssignBed(int bed, string patientId, int heartRate, int spo2)
    {
        if (bed <= 0)
            throw new ArgumentOutOfRangeException(nameof(bed));

        if (string.IsNullOrWhiteSpace(patientId))
            throw new ArgumentException("patient required");

        _bedPatient[bed] = patientId.Trim().ToUpperInvariant();

        var acuity = _acuityScorer.Score(heartRate, spo2);
        _vitalsScore[bed] = acuity;

        _pagerAlertRecorder.Record(bed, acuity);
    }

    public int ScoreAcuity(int heartRate, int spo2)
    {
        return _acuityScorer.Score(heartRate, spo2);
    }

    public string BuildHandoffNote(int bed)
    {
        if (!_bedPatient.TryGetValue(bed, out var patient))
            return $"Bed {bed}: empty";

        var acuity = _vitalsScore[bed];

        return _handoffNoteFormatter.Format(bed, patient, acuity);
    }

    public IReadOnlyList<string> DrainPagerLog()
    {
        return _pagerAlertRecorder.Drain();
    }

    public string ExportCensusCsv()
    {
        var entries = _bedPatient.Keys.Select(bed =>
            (
                Bed: bed,
                Patient: _bedPatient[bed],
                Acuity: _vitalsScore[bed]
            ));

        return _censusCsvExporter.Export(entries);
    }
}
