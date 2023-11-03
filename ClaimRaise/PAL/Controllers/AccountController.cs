using Microsoft.AspNetCore.Mvc;

namespace PAL.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
