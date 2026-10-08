using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsLab.Problems.Builder;

public sealed class CourseRegistrationBuilder
{
    private string? _studentEmail;
    private string? _courseCode;
    private string? _accessMode;
    private string? _groupCode;
    private string? _discountCode;
    private bool _sendWhatsApp;
    private bool _sendEmailWelcome;
    private string? _mentorNote;
    private DateOnly? _preferredStart;

    public CourseRegistrationBuilder ForStudent(string studentEmail)
    {
        _studentEmail = studentEmail;
        return this;
    }

    public CourseRegistrationBuilder ForCourse(string courseCode)
    {
        _courseCode = courseCode;
        return this;
    }

    public CourseRegistrationBuilder AsLiveGroup()
    {
        _accessMode = "LiveGroup";
        return this;
    }

    public CourseRegistrationBuilder AsVideosOnly()
    {
        _accessMode = "VideosOnly";
        return this;
    }

    public CourseRegistrationBuilder WithGroupCode(string groupCode)
    {
        _groupCode = groupCode;
        return this;
    }

    public CourseRegistrationBuilder WithDiscount(string discountCode)
    {
        _discountCode = discountCode;
        return this;
    }

    public CourseRegistrationBuilder SendWhatsApp()
    {
        _sendWhatsApp = true;
        return this;
    }

    public CourseRegistrationBuilder SendEmailWelcome()
    {
        _sendEmailWelcome = true;
        return this;
    }

    public CourseRegistrationBuilder WithMentorNote(string mentorNote)
    {
        _mentorNote = mentorNote;
        return this;
    }

    public CourseRegistrationBuilder StartingOn(DateOnly preferredStart)
    {
        _preferredStart = preferredStart;
        return this;
    }

    public CourseRegistration Build()
    {
        if (_accessMode == "LiveGroup" &&
            string.IsNullOrWhiteSpace(_groupCode))
        {
            throw new InvalidOperationException(
                "LiveGroup registration requires a GroupCode.");
        }

        if (_accessMode == "VideosOnly" &&
            !string.IsNullOrWhiteSpace(_groupCode))
        {
            throw new InvalidOperationException(
                "VideosOnly registration must not have a GroupCode.");
        }

        return new CourseRegistration(
            _studentEmail!,
            _courseCode!,
            _accessMode!,
            _groupCode,
            _discountCode,
            _sendWhatsApp,
            _sendEmailWelcome,
            _mentorNote,
            _preferredStart);
    }
}