using Microsoft.AspNetCore.Mvc;

namespace PAL.Controllers
{
    public class ProgramController : Controller
    {
        public IActionResult ManageProgram()
        {
            return View();
        }
    }
}
