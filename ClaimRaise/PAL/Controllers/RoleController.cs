using Microsoft.AspNetCore.Mvc;

namespace PAL.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult ManageRole()
        {
            return View();
        }
    }
}
