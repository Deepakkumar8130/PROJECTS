using Microsoft.AspNetCore.Mvc;

namespace PAL.Controllers
{
    public class UserController : Controller
    {
        public IActionResult ManageUser()
        {
            return View();
        }
    }
}
