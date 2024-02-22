using Admin.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Api.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public IActionResult Index()
        {
            return View();
        }
    }
}
