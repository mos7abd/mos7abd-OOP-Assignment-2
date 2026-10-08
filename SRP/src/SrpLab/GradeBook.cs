using SrpLab._9_GradeBook;

namespace SrpLab;

/// <summary>
/// Stores student scores and coordinates academic calculations and exports.
/// </summary>
/// <remarks>
/// Reason to change: Changes when the way student scores are recorded or stored changes.
/// </remarks>
public sealed class GradeBook
{
    private readonly Dictionary<string, List<decimal>> _scores =
        new(StringComparer.OrdinalIgnoreCase);

    private readonly GradeAverageCalculator _averageCalculator;
    private readonly LetterGradePolicy _letterGradePolicy;
    private readonly HonorRollPolicy _honorRollPolicy;
    private readonly TranscriptFormatter _transcriptFormatter;
    private readonly GradeBookCsvExporter _csvExporter;

    public GradeBook()
    {
        _averageCalculator = new GradeAverageCalculator();
        _letterGradePolicy = new LetterGradePolicy();
        _honorRollPolicy = new HonorRollPolicy();
        _transcriptFormatter = new TranscriptFormatter();
        _csvExporter = new GradeBookCsvExporter();
    }

    public void Record(string studentId, decimal score)
    {
        if (score is < 0 or > 100)
            throw new ArgumentOutOfRangeException(nameof(score));

        if (!_scores.TryGetValue(studentId, out var list))
        {
            list = new List<decimal>();
            _scores[studentId] = list;
        }

        list.Add(score);
    }

    public decimal Average(string studentId)
    {
        if (!_scores.TryGetValue(studentId, out var list))
            return 0m;

        return _averageCalculator.Calculate(list);
    }

    public string Letter(string studentId)
    {
        var average = Average(studentId);

        return _letterGradePolicy.GetLetter(average);
    }

    public bool MeetsHonorRoll(string studentId)
    {
        var average = Average(studentId);
        var letter = Letter(studentId);

        return _honorRollPolicy.MeetsHonorRoll(
            average,
            letter);
    }

    public string TranscriptPlain(
        string studentId,
        string fullName)
    {
        var average = Average(studentId);
        var letter = Letter(studentId);
        var honor = MeetsHonorRoll(studentId);

        return _transcriptFormatter.Format(
            studentId,
            fullName,
            average,
            letter,
            honor);
    }

    public string ExportCsv()
    {
        var students = _scores.Keys.Select(studentId =>
        {
            var average = Average(studentId);
            var letter = Letter(studentId);
            var honor = MeetsHonorRoll(studentId);

            return (
                StudentId: studentId,
                Average: average,
                Letter: letter,
                Honor: honor);
        });

        return _csvExporter.Export(students);
    }
}