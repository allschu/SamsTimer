using Microsoft.AspNetCore.Mvc;

namespace WebAdmin.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
