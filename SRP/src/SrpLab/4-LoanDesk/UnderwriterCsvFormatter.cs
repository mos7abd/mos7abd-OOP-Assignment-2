using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._4_LoanDesk
{
    /// <summary>
    /// Formats loan application data as a CSV row for the underwriter.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when the underwriter CSV export format changes.
    /// </remarks>
    public sealed class UnderwriterCsvFormatter
    {
        public string Format(
            string applicationId,
            int creditScore,
            int employmentMonths,
            bool hasCollateral,
            decimal riskScore,
            bool isEligible)
        {
            return $"{applicationId}," +
                   $"{creditScore}," +
                   $"{employmentMonths}," +
                   $"{(hasCollateral ? 1 : 0)}," +
                   $"{riskScore:0.00}," +
                   $"{(isEligible ? "Y" : "N")}";
        }
    }
}
