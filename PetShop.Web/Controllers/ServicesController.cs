using Microsoft.AspNetCore.Mvc;
using PetShop.Web.Data;
using PetShop.Web.Models;

namespace PetShop.Web.Controllers
{
    public class ServicesController : Controller
    {
        private readonly AppDbContext _context;

        public ServicesController(AppDbContext context)
        {
            _context = context;
        }

        private bool UsuarioEhAdmin()
        {
            return HttpContext.Session.GetString("Role") == "Admin";
        }

        public IActionResult Index()
        {
            var servicos = _context.Servicos.ToList();

            return View(servicos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!UsuarioEhAdmin())
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(Servico servico)
        {
            if (!UsuarioEhAdmin())
            {
                return RedirectToAction("Login", "Auth");
            }

            _context.Servicos.Add(servico);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!UsuarioEhAdmin())
            {
                return RedirectToAction("Login", "Auth");
            }

            var servico = _context.Servicos.FirstOrDefault(s => s.Id == id);

            if (servico == null)
            {
                return RedirectToAction("Index");
            }

            return View(servico);
        }

        [HttpPost]
        public IActionResult Edit(Servico servico)
        {
            if (!UsuarioEhAdmin())
            {
                return RedirectToAction("Login", "Auth");
            }

            var servicoBanco = _context.Servicos.FirstOrDefault(s => s.Id == servico.Id);

            if (servicoBanco == null)
            {
                return RedirectToAction("Index");
            }

            servicoBanco.Nome = servico.Nome;
            servicoBanco.Descricao = servico.Descricao;
            servicoBanco.Preco = servico.Preco;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!UsuarioEhAdmin())
            {
                return RedirectToAction("Login", "Auth");
            }

            var servico = _context.Servicos.FirstOrDefault(s => s.Id == id);

            if (servico == null)
            {
                return RedirectToAction("Index");
            }

            return View(servico);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            if (!UsuarioEhAdmin())
            {
                return RedirectToAction("Login", "Auth");
            }

            var servico = _context.Servicos.FirstOrDefault(s => s.Id == id);

            if (servico == null)
            {
                return RedirectToAction("Index");
            }

            _context.Servicos.Remove(servico);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}