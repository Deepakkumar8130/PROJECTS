using Microsoft.AspNetCore.Mvc;

namespace PAL.Controllers
{
    public class ClaimController : Controller
    {
        public IActionResult AddClaim()
        {
            return View();
        }
    }
}
