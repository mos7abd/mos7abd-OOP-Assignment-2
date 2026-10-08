using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._4_LoanDesk
{

    /// <summary>
    /// Determines which documents are required for a loan application.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when the required loan documents or compliance rules change.
    /// </remarks>
    public sealed class LoanDocumentRequirements
    {
        public IReadOnlyList<string> GetRequiredDocuments(
            decimal requestedAmount,
            bool hasCollateral,
            int employmentMonths,
            bool isEligible)
        {
            var documents = new List<string>
        {
            "National ID",
            "Proof of income (3 months)"
        };

            if (requestedAmount > 40_000m)
                documents.Add("Bank statements (6 months)");

            if (hasCollateral)
                documents.Add("Collateral ownership deed");

            if (employmentMonths < 12)
                documents.Add("Employer letter");

            if (!isEligible)
                documents.Add("Manual underwriter referral form");

            return documents;
        }
    }
}
