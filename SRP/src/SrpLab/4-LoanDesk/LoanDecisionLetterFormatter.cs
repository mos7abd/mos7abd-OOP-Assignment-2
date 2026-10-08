using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrpLab._4_LoanDesk
{

    /// <summary>
    /// Formats the decision letter for a loan applicant.
    /// </summary>
    /// <remarks>
    /// Reason to change: Changes when the wording or format of the applicant decision letter changes.
    /// </remarks>
    public sealed class LoanDecisionLetterFormatter
    {
        public string Format(
            string applicantName,
            decimal requestedAmount,
            decimal riskScore,
            bool isEligible,
            IReadOnlyList<string> requiredDocuments)
        {
            if (isEligible)
            {
                return $"Dear {applicantName},\n" +
                       $"Your request for {requestedAmount:C} is pre-approved (risk {riskScore:0}).\n" +
                       $"Please upload: {string.Join("; ", requiredDocuments)}.\n";
            }

            return $"Dear {applicantName},\n" +
                   $"We are unable to approve {requestedAmount:C} at this time.\n" +
                   $"Reference risk={riskScore:0}. You may reapply after improving documentation.\n";
        }
    }
}
