using SrpLab._4_LoanDesk;

namespace SrpLab;

/// <summary>
/// Manages loan application data and coordinates loan-related operations.
/// </summary>
/// <remarks>
/// Reason to change: Changes when the way loan application data and operations are coordinated changes.
/// </remarks>
public sealed class LoanDesk
{
    public decimal RequestedAmount { get; }
    public int CreditScore { get; }
    public int EmploymentMonths { get; }
    public bool HasCollateral { get; }

    private readonly LoanRiskCalculator _riskCalculator;
    private readonly LoanDocumentRequirements _documentRequirements;
    private readonly LoanDecisionLetterFormatter _decisionLetterFormatter;
    private readonly UnderwriterCsvFormatter _underwriterCsvFormatter;

    public LoanDesk(
        decimal requestedAmount,
        int creditScore,
        int employmentMonths,
        bool hasCollateral)
    {
        RequestedAmount = requestedAmount;
        CreditScore = creditScore;
        EmploymentMonths = employmentMonths;
        HasCollateral = hasCollateral;

        _riskCalculator = new LoanRiskCalculator();
        _documentRequirements = new LoanDocumentRequirements();
        _decisionLetterFormatter = new LoanDecisionLetterFormatter();
        _underwriterCsvFormatter = new UnderwriterCsvFormatter();
    }

    public decimal RiskScore()
    {
        return _riskCalculator.CalculateRiskScore(
            RequestedAmount,
            CreditScore,
            EmploymentMonths,
            HasCollateral);
    }

    public bool IsEligible()
    {
        return _riskCalculator.IsEligible(
            RequestedAmount,
            CreditScore,
            EmploymentMonths,
            HasCollateral);
    }

    public IReadOnlyList<string> RequiredDocuments()
    {
        return _documentRequirements.GetRequiredDocuments(
            RequestedAmount,
            HasCollateral,
            EmploymentMonths,
            IsEligible());
    }

    public string DecisionLetter(string applicantName)
    {
        return _decisionLetterFormatter.Format(
            applicantName,
            RequestedAmount,
            RiskScore(),
            IsEligible(),
            RequiredDocuments());
    }

    public string UnderwriterCsvRow(string applicationId)
    {
        return _underwriterCsvFormatter.Format(
            applicationId,
            CreditScore,
            EmploymentMonths,
            HasCollateral,
            RiskScore(),
            IsEligible());
    }
}
