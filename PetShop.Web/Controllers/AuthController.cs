using Microsoft.AspNetCore.Mvc;

namespace PetShop.Web.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            return RedirectToAction("Dashboard", "Admin");
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}