using MAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IClaimService
    {
        Task<string> RaiseClaimRequest(ClaimRequestModel claim);
        Task<Tuple<string, List<ClaimRequest>>> GetAllPendingRequests(int UserId, string Role);
        Task<Tuple<string, List<ClaimActionHistory>>> GetClaimHistory(int ClaimId);
        Task<string> ActionOnRequest(ClaimAction claim);
        Task<Tuple<string, byte[]>> GetClaimEvidence(string path);
    }
}
