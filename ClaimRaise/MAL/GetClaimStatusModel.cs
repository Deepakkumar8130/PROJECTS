using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAL
{
    public class GetClaimStatusModel
    {
        public string ClaimId { get; set; }
        public string ClaimTitle { get; set; }
        public string ClaimReason { get; set; }
        public string ClaimAmount { get; set; }
        public string ClaimDt { get; set; }
        public string Action { get; set; }
        public string ActionBy { get; set; }
        public string CurrentStatus { get; set; }
    }
}
