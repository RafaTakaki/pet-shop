using Microsoft.AspNetCore.Mvc;
using PetShop.Web.Data;
using PetShop.Web.Models;

namespace PetShop.Web.Controllers
{
    public class PetsController : Controller
    {
        private readonly AppDbContext _context;

        public PetsController(AppDbContext context)
        {
            _context = context;
        }

        private bool UsuarioEhAdmin()
        {
            return HttpContext.Session.GetString("Role") == "Admin";
        }

        public IActionResult Index()
        {
            var tutorId = HttpContext.Session.GetInt32("TutorId");

            if (tutorId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            if (UsuarioEhAdmin())
            {
                var todosPets = _context.Pets.ToList();

                return View(todosPets);
            }

            var pets = _context.Pets
                .Where(p => p.TutorId == tutorId)
                .ToList();

            return View(pets);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (UsuarioEhAdmin())
            {
                return RedirectToAction("Index");
            }

            var tutorId = HttpContext.Session.GetInt32("TutorId");

            if (tutorId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(Pet pet)
        {
            if (UsuarioEhAdmin())
            {
                return RedirectToAction("Index");
            }

            var tutorId = HttpContext.Session.GetInt32("TutorId");

            if (tutorId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            pet.TutorId = tutorId.Value;

            _context.Pets.Add(pet);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var tutorId = HttpContext.Session.GetInt32("TutorId");

            if (tutorId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var pet = UsuarioEhAdmin()
                ? _context.Pets.FirstOrDefault(p => p.Id == id)
                : _context.Pets.FirstOrDefault(p => p.Id == id && p.TutorId == tutorId);

            if (pet == null)
            {
                return RedirectToAction("Index");
            }

            return View(pet);
        }

        [HttpPost]
        public IActionResult Edit(Pet pet)
        {
            var tutorId = HttpContext.Session.GetInt32("TutorId");

            if (tutorId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var petBanco = UsuarioEhAdmin()
                ? _context.Pets.FirstOrDefault(p => p.Id == pet.Id)
                : _context.Pets.FirstOrDefault(p => p.Id == pet.Id && p.TutorId == tutorId);

            if (petBanco == null)
            {
                return RedirectToAction("Index");
            }

            petBanco.Nome = pet.Nome;
            petBanco.Especie = pet.Especie;
            petBanco.Raca = pet.Raca;
            petBanco.Idade = pet.Idade;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var tutorId = HttpContext.Session.GetInt32("TutorId");

            if (tutorId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var pet = UsuarioEhAdmin()
                ? _context.Pets.FirstOrDefault(p => p.Id == id)
                : _context.Pets.FirstOrDefault(p => p.Id == id && p.TutorId == tutorId);

            if (pet == null)
            {
                return RedirectToAction("Index");
            }

            return View(pet);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            var tutorId = HttpContext.Session.GetInt32("TutorId");

            if (tutorId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var pet = UsuarioEhAdmin()
                ? _context.Pets.FirstOrDefault(p => p.Id == id)
                : _context.Pets.FirstOrDefault(p => p.Id == id && p.TutorId == tutorId);

            if (pet == null)
            {
                return RedirectToAction("Index");
            }

            _context.Pets.Remove(pet);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}