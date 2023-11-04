using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAL
{
    public class ClaimRequestModel
    {
        public int UserId { get; set; }
        public string ClaimTitle { get; set; }
        public string ClaimReason { get; set; }
        public string ClaimDescription { get; set; }
        public string ClaimAmount { get; set; }
        public string Evidence { get; set; }
        public string ExpenseDt { get; set; }
    }
}
