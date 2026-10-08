using SrpLab._3_SupportTicket;

namespace SrpLab;

/// <summary>
/// Manages support ticket data and coordinates ticket-related operations.
/// </summary>
/// <remarks>
/// Reason to change: Changes when the way support ticket data and customer messages are managed changes.
/// </remarks>
public sealed class SupportTicket
{
    public string Id { get; }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public DateTimeOffset OpenedAt { get; }
    public string Priority { get; private set; } = "P3";

    private readonly TicketPriorityClassifier _priorityClassifier;
    private readonly SlaCalculator _slaCalculator;
    private readonly PublicReplyFormatter _publicReplyFormatter;
    private readonly EscalationMessageFormatter _escalationMessageFormatter;

    public SupportTicket(
        string id,
        string subject,
        string body,
        DateTimeOffset openedAt)
    {
        Id = id;
        Subject = subject;
        Body = body;
        OpenedAt = openedAt;

        _priorityClassifier = new TicketPriorityClassifier();
        _slaCalculator = new SlaCalculator();
        _publicReplyFormatter = new PublicReplyFormatter();
        _escalationMessageFormatter = new EscalationMessageFormatter();

        RecalculatePriorityFromText();
    }

    public void AppendCustomerMessage(string text)
    {
        Body += "\n---\n" + text;
        RecalculatePriorityFromText();
    }

    public void RecalculatePriorityFromText()
    {
        Priority = _priorityClassifier.Classify(
            Subject,
            Body);
    }

    public DateTimeOffset SlaDeadline()
    {
        return _slaCalculator.CalculateDeadline(
            OpenedAt,
            Priority);
    }

    public bool IsBreached(DateTimeOffset now)
    {
        return _slaCalculator.IsBreached(
            OpenedAt,
            Priority,
            now);
    }

    public string DraftPublicReply(string agentName)
    {
        return _publicReplyFormatter.Format(
            Id,
            Priority,
            agentName,
            SlaDeadline());
    }

    public string InternalEscalationBlurb()
    {
        return _escalationMessageFormatter.Format(
            Id,
            Priority,
            SlaDeadline());
    }
}
