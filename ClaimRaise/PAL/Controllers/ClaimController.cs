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
        
        //public async Task<IActionResult> DownloadEvidence(string path)
        //{
        //    return RedirectToAction();
        //}
    }
}
