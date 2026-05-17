using Microsoft.AspNetCore.Mvc;

namespace PetShop.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}