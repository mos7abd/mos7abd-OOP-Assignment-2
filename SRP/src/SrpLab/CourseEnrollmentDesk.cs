using SrpLab._5_CourseEnrollmentDesk;

namespace SrpLab;

/// <summary>
/// Manages course enrollment data and coordinates enrollment-related operations.
/// </summary>
/// <remarks>
/// Reason to change: Changes when the way course enrollment data and operations are coordinated changes.
/// </remarks>
public sealed class CourseEnrollmentDesk
{
    public int Capacity { get; }
    public decimal Tuition { get; }
    public string CourseCode { get; }

    private readonly CourseEnrollmentManager _enrollmentManager;
    private readonly WelcomePacketFormatter _welcomePacketFormatter;
    private readonly TuitionInvoiceFormatter _tuitionInvoiceFormatter;

    public CourseEnrollmentDesk(
        string courseCode,
        int capacity,
        decimal tuition)
    {
        CourseCode = courseCode;
        Capacity = capacity;
        Tuition = tuition;

        _enrollmentManager = new CourseEnrollmentManager(Capacity);
        _welcomePacketFormatter = new WelcomePacketFormatter();
        _tuitionInvoiceFormatter = new TuitionInvoiceFormatter();
    }

    public string Register(string studentEmail)
    {
        return _enrollmentManager.Register(studentEmail);
    }

    public int WaitlistPosition(string studentEmail)
    {
        return _enrollmentManager.WaitlistPosition(studentEmail);
    }

    public string WelcomePacketMarkdown(
        string studentEmail,
        string studentName)
    {
        var status = _enrollmentManager.IsSeated(studentEmail)
            ? "confirmed seat"
            : $"waitlist #{WaitlistPosition(studentEmail)}";

        return _welcomePacketFormatter.Format(
            CourseCode,
            studentName,
            status);
    }

    public string TuitionInvoiceLine(string studentEmail)
    {
        var isSeated = _enrollmentManager.IsSeated(studentEmail);

        return _tuitionInvoiceFormatter.Format(
            CourseCode,
            Tuition,
            isSeated);
    }

    public void PromoteFromWaitlist(int seats)
    {
        _enrollmentManager.PromoteFromWaitlist(seats);
    }
}
