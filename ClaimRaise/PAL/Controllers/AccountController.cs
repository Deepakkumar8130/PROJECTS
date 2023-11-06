using Microsoft.AspNetCore.Mvc;

namespace PAL.Controllers
{
    public class AccountController : Controller
    {
        public async Task<IActionResult> Login()
        {
            return View();
        }
    }
}
