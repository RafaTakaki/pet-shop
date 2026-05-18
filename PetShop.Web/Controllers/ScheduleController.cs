using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShop.Web.Data;
using PetShop.Web.Models;

namespace PetShop.Web.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly AppDbContext _context;

        public ScheduleController(AppDbContext context)
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

            var reservas = UsuarioEhAdmin()
                ? _context.Reservas
                    .Include(r => r.Pet)
                    .ThenInclude(p => p!.Tutor)
                    .ToList()
                : _context.Reservas
                    .Include(r => r.Pet)
                    .Where(r => r.Pet!.TutorId == tutorId)
                    .ToList();

            return View(reservas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var tutorId = HttpContext.Session.GetInt32("TutorId");

            if (tutorId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            if (UsuarioEhAdmin())
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            ViewBag.Pets = _context.Pets
                .Where(p => p.TutorId == tutorId)
                .ToList();

            ViewBag.Servicos = _context.Servicos
                .ToList();

            ViewBag.Horarios = new List<string>
            {
                "09:00",
                "10:00",
                "14:00",
                "16:00"
            };

            return View();
        }

        [HttpPost]
        public IActionResult Create(Reserva reserva)
        {
            var tutorId = HttpContext.Session.GetInt32("TutorId");

            if (tutorId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            if (UsuarioEhAdmin())
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            var reservaExistente = _context.Reservas
                .Any(r =>
                    r.Data.Date == reserva.Data.Date &&
                    r.Horario == reserva.Horario);

            if (reservaExistente)
            {
                ViewBag.Erro = "Já existe um agendamento para este horário.";

                ViewBag.Pets = _context.Pets
                    .Where(p => p.TutorId == tutorId)
                    .ToList();

                ViewBag.Servicos = _context.Servicos
                    .ToList();

                ViewBag.Horarios = new List<string>
                {
                    "09:00",
                    "10:00",
                    "14:00",
                    "16:00"
                };

                return View(reserva);
            }

            _context.Reservas.Add(reserva);

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

            var reserva = UsuarioEhAdmin()
                ? _context.Reservas
                    .Include(r => r.Pet)
                    .FirstOrDefault(r => r.Id == id)
                : _context.Reservas
                    .Include(r => r.Pet)
                    .FirstOrDefault(r =>
                        r.Id == id &&
                        r.Pet!.TutorId == tutorId);

            if (reserva == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Pets = UsuarioEhAdmin()
                ? _context.Pets.ToList()
                : _context.Pets
                    .Where(p => p.TutorId == tutorId)
                    .ToList();

            ViewBag.Servicos = _context.Servicos
                .ToList();

            ViewBag.Horarios = new List<string>
            {
                "09:00",
                "10:00",
                "14:00",
                "16:00"
            };

            return View(reserva);
        }

        [HttpPost]
        public IActionResult Edit(Reserva reserva)
        {
            var tutorId = HttpContext.Session.GetInt32("TutorId");

            if (tutorId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var reservaExistente = _context.Reservas
                .Any(r =>
                    r.Id != reserva.Id &&
                    r.Data.Date == reserva.Data.Date &&
                    r.Horario == reserva.Horario);

            if (reservaExistente)
            {
                ViewBag.Erro = "Já existe um agendamento para este horário.";

                ViewBag.Pets = UsuarioEhAdmin()
                    ? _context.Pets.ToList()
                    : _context.Pets
                        .Where(p => p.TutorId == tutorId)
                        .ToList();

                ViewBag.Servicos = _context.Servicos
                    .ToList();

                ViewBag.Horarios = new List<string>
                {
                    "09:00",
                    "10:00",
                    "14:00",
                    "16:00"
                };

                return View(reserva);
            }

            var reservaBanco = UsuarioEhAdmin()
                ? _context.Reservas
                    .FirstOrDefault(r => r.Id == reserva.Id)
                : _context.Reservas
                    .Include(r => r.Pet)
                    .FirstOrDefault(r =>
                        r.Id == reserva.Id &&
                        r.Pet!.TutorId == tutorId);

            if (reservaBanco == null)
            {
                return RedirectToAction("Index");
            }

            reservaBanco.PetId = reserva.PetId;
            reservaBanco.Servico = reserva.Servico;
            reservaBanco.Data = reserva.Data;
            reservaBanco.Horario = reserva.Horario;

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

            var reserva = UsuarioEhAdmin()
                ? _context.Reservas
                    .Include(r => r.Pet)
                    .ThenInclude(p => p!.Tutor)
                    .FirstOrDefault(r => r.Id == id)
                : _context.Reservas
                    .Include(r => r.Pet)
                    .FirstOrDefault(r =>
                        r.Id == id &&
                        r.Pet!.TutorId == tutorId);

            if (reserva == null)
            {
                return RedirectToAction("Index");
            }

            return View(reserva);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            var tutorId = HttpContext.Session.GetInt32("TutorId");

            if (tutorId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var reserva = UsuarioEhAdmin()
                ? _context.Reservas
                    .FirstOrDefault(r => r.Id == id)
                : _context.Reservas
                    .Include(r => r.Pet)
                    .FirstOrDefault(r =>
                        r.Id == id &&
                        r.Pet!.TutorId == tutorId);

            if (reserva == null)
            {
                return RedirectToAction("Index");
            }

            _context.Reservas.Remove(reserva);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetHorariosDisponiveis(DateTime data)
        {
            var todosHorarios = new List<string>
            {
                "09:00",
                "10:00",
                "14:00",
                "16:00"
            };

            var horariosOcupados = _context.Reservas
                .Where(r => r.Data.Date == data.Date)
                .Select(r => r.Horario)
                .ToList();

            var horariosDisponiveis = todosHorarios
                .Where(h => !horariosOcupados.Contains(h))
                .ToList();

            return Json(horariosDisponiveis);
        }

        [HttpGet]
        public IActionResult GetHorariosDisponiveisEdicao(DateTime data, int reservaId)
        {
            var todosHorarios = new List<string>
            {
                "09:00",
                "10:00",
                "14:00",
                "16:00"
            };

            var horariosOcupados = _context.Reservas
                .Where(r =>
                    r.Data.Date == data.Date &&
                    r.Id != reservaId)
                .Select(r => r.Horario)
                .ToList();

            var horariosDisponiveis = todosHorarios
                .Where(h => !horariosOcupados.Contains(h))
                .ToList();

            return Json(horariosDisponiveis);
        }
    }
}