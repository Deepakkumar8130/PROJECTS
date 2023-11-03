using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROJECT6.Models;
using System.Security.Claims;

namespace PROJECT6.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if(ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    NormalizedUserName = model.Email,
                    NormalizedEmail = model.Email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user,model.Password);
                if(result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach(var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }

            }
            return View(model);
        }
        
        
        
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]        
        
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if(user == null)
                {
                    ModelState.AddModelError("", "User Not Found");
                }
                else
                {
                    var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(user,false);
                        var claim = new Claim("usermail",model.Email);
                        await userManager.AddClaimAsync(user, claim);
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Login Credential");
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
