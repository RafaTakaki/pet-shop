using Microsoft.AspNetCore.Mvc;
using PetShop.Web.Data;
using PetShop.Web.Models;
using PetShop.Web.Services;

namespace PetShop.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            CriarAdminSeNaoExistir();

            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var senhaHash = PasswordService.GerarHash(senha);

            var tutor = _context.Tutores
                .FirstOrDefault(t => t.Email == email && t.SenhaHash == senhaHash);

            if (tutor == null)
            {
                ViewBag.Erro = "Email ou senha inválidos.";
                return View();
            }

            HttpContext.Session.SetInt32("TutorId", tutor.Id);
            HttpContext.Session.SetString("TutorNome", tutor.Nome);
            HttpContext.Session.SetString("Role", tutor.Role);

            if (tutor.Role == "Admin")
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string nome, string email, string senha)
        {
            var emailJaExiste = _context.Tutores.Any(t => t.Email == email);

            if (emailJaExiste)
            {
                ViewBag.Erro = "Este email já está cadastrado.";
                return View();
            }

            var tutor = new Tutor
            {
                Nome = nome,
                Email = email,
                SenhaHash = PasswordService.GerarHash(senha),
                Role = "Tutor"
            };

            _context.Tutores.Add(tutor);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }

        private void CriarAdminSeNaoExistir()
        {
            var existeAdmin = _context.Tutores.Any(t => t.Role == "Admin");

            if (existeAdmin)
            {
                return;
            }

            var admin = new Tutor
            {
                Nome = "Administrador",
                Email = "admin@petshop.com",
                SenhaHash = PasswordService.GerarHash("admin123"),
                Role = "Admin"
            };

            _context.Tutores.Add(admin);
            _context.SaveChanges();
        }
    }
}