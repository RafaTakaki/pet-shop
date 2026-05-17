using Microsoft.AspNetCore.Mvc;

namespace PetShop.Web.Controllers
{
    public class ScheduleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int? id)
        {
            return View();
        }

        public IActionResult Delete(int? id)
        {
            return View();
        }

        public IActionResult Details(int? id)
        {
            return View();
        }
    }
}