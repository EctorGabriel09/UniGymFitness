using Microsoft.AspNetCore.Mvc;
using UniGymFitness.Data;

namespace UniGymFitness.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tipoUsuario = HttpContext.Session.GetString("TipoUsuario");

            if (tipoUsuario != "Administrador")
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.TotalUsuarios = _context.Usuarios.Count();
            ViewBag.NomeUsuario = HttpContext.Session.GetString("NomeUsuario");

            return View();
        }
    }
}