using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShop.Web.Data;

namespace PetShop.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        private bool UsuarioEhAdmin()
        {
            return HttpContext.Session.GetString("Role") == "Admin";
        }

        public IActionResult Dashboard()
        {
            if (!UsuarioEhAdmin())
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.TotalTutores = _context.Tutores
                .Count(t => t.Role == "Tutor");

            ViewBag.TotalPets = _context.Pets.Count();

            ViewBag.TotalReservas = _context.Reservas.Count();

            ViewBag.ReservasHoje = _context.Reservas
                .Count(r => r.Data.Date == DateTime.Today);

            var ultimasReservas = _context.Reservas
                .Include(r => r.Pet)
                .ThenInclude(p => p!.Tutor)
                .OrderByDescending(r => r.Data)
                .ThenByDescending(r => r.Horario)
                .Take(10)
                .ToList();

            return View(ultimasReservas);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!UsuarioEhAdmin())
            {
                return RedirectToAction("Login", "Auth");
            }

            var reserva = _context.Reservas
                .Include(r => r.Pet)
                .FirstOrDefault(r => r.Id == id);

            if (reserva == null)
            {
                return RedirectToAction("Dashboard");
            }

            ViewBag.Pets = _context.Pets.ToList();
            ViewBag.Servicos = _context.Servicos.ToList();

            return View(reserva);
        }

        [HttpPost]
        public IActionResult Edit(PetShop.Web.Models.Reserva reserva)
        {
            if (!UsuarioEhAdmin())
            {
                return RedirectToAction("Login", "Auth");
            }

            var reservaBanco = _context.Reservas
                .FirstOrDefault(r => r.Id == reserva.Id);

            if (reservaBanco == null)
            {
                return RedirectToAction("Dashboard");
            }

            reservaBanco.PetId = reserva.PetId;
            reservaBanco.Servico = reserva.Servico;
            reservaBanco.Data = reserva.Data;
            reservaBanco.Horario = reserva.Horario;
            reservaBanco.Status = reserva.Status;

            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!UsuarioEhAdmin())
            {
                return RedirectToAction("Login", "Auth");
            }

            var reserva = _context.Reservas
                .Include(r => r.Pet)
                .ThenInclude(p => p!.Tutor)
                .FirstOrDefault(r => r.Id == id);

            if (reserva == null)
            {
                return RedirectToAction("Dashboard");
            }

            return View(reserva);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            if (!UsuarioEhAdmin())
            {
                return RedirectToAction("Login", "Auth");
            }

            var reserva = _context.Reservas
                .FirstOrDefault(r => r.Id == id);

            if (reserva == null)
            {
                return RedirectToAction("Dashboard");
            }

            _context.Reservas.Remove(reserva);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }
    }
}