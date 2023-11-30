using Microsoft.AspNetCore.Mvc;

namespace PAL.Controllers
{
    public class ClaimController : Controller
    {
        public async Task<IActionResult> AddClaim()
        {
            return View();
        }
        
        public async Task<IActionResult> ShowClaim()
        {
            return View();
        }
        public async Task<IActionResult> ShowClaimTransaction()
        {
            return View();
        }

        public async Task<IActionResult> ClaimStatus()
        {
            return View();
        }
    }
}
