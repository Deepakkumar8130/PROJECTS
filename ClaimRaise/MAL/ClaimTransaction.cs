using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAL
{
    public class ClaimTransaction
    {
        public string TransactionNo { get; set; }
        public string ClaimTitle { get; set; }
        public string ClaimReason { get; set; }
        public string ClaimAmount { get; set; }
        public string ClaimDescription { get; set; }
        public string ClaimDt { get; set; }
        public string TransactionDt { get; set; }
        public string ApprovedBy { get; set; }
    }
}
