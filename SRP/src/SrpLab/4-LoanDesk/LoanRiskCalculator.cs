using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._4_LoanDesk
{

    /// <summary>
    /// Calculates loan risk scores and determines loan eligibility.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when the loan risk and eligibility rules change.
    /// </remarks>
    public sealed class LoanRiskCalculator
    {
        public decimal CalculateRiskScore(
            decimal requestedAmount,
            int creditScore,
            int employmentMonths,
            bool hasCollateral)
        {
            decimal score = 100m;

            score -= Math.Max(0, 700 - creditScore) * 0.15m;

            if (employmentMonths < 6)
                score -= 20m;

            if (requestedAmount > 50_000m && !hasCollateral)
                score -= 25m;

            if (requestedAmount > 150_000m)
                score -= 10m;

            return Math.Clamp(score, 0m, 100m);
        }

        public bool IsEligible(
            decimal requestedAmount,
            int creditScore,
            int employmentMonths,
            bool hasCollateral)
        {
            var riskScore = CalculateRiskScore(
                requestedAmount,
                creditScore,
                employmentMonths,
                hasCollateral);

            return riskScore >= 55m && creditScore >= 580;
        }
    }
}
