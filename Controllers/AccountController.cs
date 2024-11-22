using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROG6212_POE_CMCS.Models;
using System.Threading.Tasks;

namespace PROG6212_POE_CMCS.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        // Redirect based on the user's role
                        var roles = await _userManager.GetRolesAsync(user);
                        if (roles.Contains("Admin") || roles.Contains("ProgramCoordinator"))
                        {
                            return RedirectToAction("HRView", "Claim");
                        }
                        else if (roles.Contains("Lecturer"))
                        {
                            return RedirectToAction("Create", "Claim");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]  // Optional: Add anti-forgery token to prevent CSRF
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();  // Sign out the user
            return RedirectToAction("Login", "Account");  // Redirect to the login page after logout
        }

    }
}
