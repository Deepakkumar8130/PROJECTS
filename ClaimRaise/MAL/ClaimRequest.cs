using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAL
{
    public class ClaimRequest
    {
        public int ClaimId { get; set; }
        public string EmployeeName { get; set; }
        public string ClaimTitle { get; set; }
        public string ClaimReason { get; set; }
        public string ClaimDescription { get; set; }
        public string ClaimAmount { get; set; }
        public string ClaimEvidence { get; set; }
        public string ClaimExpenseDt { get; set; }
        public string ClaimDt { get; set; }
        public string CurrentStatus { get; set; }
    }
}
