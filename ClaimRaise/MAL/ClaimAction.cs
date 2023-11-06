using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAL
{
    public class ClaimAction
    {
        public int ClaimId { get; set; }
        public int Action { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; }
        public string Remarks { get; set; }
    }
}
