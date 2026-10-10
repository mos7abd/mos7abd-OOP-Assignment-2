using System;
using System.Collections.Generic;

namespace SrpLab._10_AppointmentDesk;

public sealed class AppointmentDesk
{
    private readonly HashSet<DateTimeOffset> _booked = new();

    private readonly BusinessHoursPolicy _businessHoursPolicy;
    private readonly IcsCalendarFormatter _icsCalendarFormatter;
    private readonly SmsReminderFormatter _smsReminderFormatter;

    public TimeOnly Open { get; }
    public TimeOnly Close { get; }
    public int SlotMinutes { get; }

    public AppointmentDesk(
        TimeOnly open,
        TimeOnly close,
        int slotMinutes)
    {
        Open = open;
        Close = close;
        SlotMinutes = slotMinutes;

        _businessHoursPolicy = new BusinessHoursPolicy();
        _icsCalendarFormatter = new IcsCalendarFormatter();
        _smsReminderFormatter = new SmsReminderFormatter();
    }

    public bool IsWithinBusinessHours(DateTimeOffset when)
    {
        return _businessHoursPolicy.IsWithin(
            when,
            Open,
            Close,
            SlotMinutes);
    }

    public DateTimeOffset? FindNextSlot(
        DateTimeOffset from,
        int searchHours)
    {
        var cursor = Align(from);
        var end = from.AddHours(searchHours);

        while (cursor < end)
        {
            if (IsWithinBusinessHours(cursor) &&
                !_booked.Contains(cursor))
            {
                return cursor;
            }

            cursor = cursor.AddMinutes(SlotMinutes);
        }

        return null;
    }

    public bool TryBook(DateTimeOffset slot)
    {
        if (!IsWithinBusinessHours(slot) ||
            _booked.Contains(slot))
        {
            return false;
        }

        _booked.Add(slot);
        return true;
    }

    public string ToIcs(
        DateTimeOffset slot,
        string patientName,
        string clinician)
    {
        return _icsCalendarFormatter.Format(
            slot,
            SlotMinutes,
            patientName,
            clinician);
    }

    public string SmsReminder(
        DateTimeOffset slot,
        string clinicPhone)
    {
        return _smsReminderFormatter.Format(
            slot,
            clinicPhone);
    }

    private DateTimeOffset Align(DateTimeOffset from)
    {
        var minutes = from.Minute - (from.Minute % SlotMinutes);

        return new DateTimeOffset(
            from.Year,
            from.Month,
            from.Day,
            from.Hour,
            minutes,
            0,
            from.Offset);
    }
}