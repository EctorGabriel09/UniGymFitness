using Microsoft.AspNetCore.Mvc;
using UniGymFitness.Data;
using UniGymFitness.Models;

namespace UniGymFitness.Controllers
{
    public class PlanosController : Controller
    {
        private readonly AppDbContext _context;

        public PlanosController(AppDbContext context)
        {
            _context = context;
        }

        private bool UsuarioEhAdministrador()
        {
            return HttpContext.Session.GetString("TipoUsuario") == "Administrador";
        }

        public IActionResult Index()
        {
            if (!UsuarioEhAdministrador())
            {
                return RedirectToAction("Login", "Account");
            }

            var planos = _context.Planos
                .OrderBy(p => p.Nome)
                .ToList();

            return View(planos);
        }

        public IActionResult Criar()
        {
            if (!UsuarioEhAdministrador())
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(Plano plano)
        {
            if (!UsuarioEhAdministrador())
            {
                return RedirectToAction("Login", "Account");
            }

            plano.Nome = plano.Nome?.Trim() ?? string.Empty;
            plano.Descricao = plano.Descricao?.Trim() ?? string.Empty;
            plano.Beneficios = plano.Beneficios?.Trim() ?? string.Empty;

            if (!ModelState.IsValid)
            {
                return View(plano);
            }

            _context.Planos.Add(plano);
            _context.SaveChanges();

            TempData["Sucesso"] = "Plano cadastrado com sucesso!";

            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            if (!UsuarioEhAdministrador())
            {
                return RedirectToAction("Login", "Account");
            }

            var plano = _context.Planos.Find(id);

            if (plano == null)
                return NotFound();

            return View(plano);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Plano plano)
        {
            if (!UsuarioEhAdministrador())
            {
                return RedirectToAction("Login", "Account");
            }

            plano.Nome = plano.Nome?.Trim() ?? string.Empty;
            plano.Descricao = plano.Descricao?.Trim() ?? string.Empty;
            plano.Beneficios = plano.Beneficios?.Trim() ?? string.Empty;

            if (ModelState.IsValid)
            {
                _context.Planos.Update(plano);
                _context.SaveChanges();

                TempData["Sucesso"] = "Plano atualizado com sucesso!";

                return RedirectToAction("Index");
            }

            return View(plano);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Excluir(int id)
        {
            if (!UsuarioEhAdministrador())
            {
                return RedirectToAction("Login", "Account");
            }

            var plano = _context.Planos.Find(id);

            if (plano == null)
                return NotFound();

            _context.Planos.Remove(plano);
            _context.SaveChanges();

            TempData["Sucesso"] = "Plano excluído com sucesso!";

            return RedirectToAction("Index");
        }
    }
}
