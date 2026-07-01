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
            // Total de usuários cadastrados
            ViewBag.TotalUsuarios = _context.Usuarios.Count();

            // Nome do usuário logado
            ViewBag.NomeUsuario = HttpContext.Session.GetString("NomeUsuario");

            return View();
        }
    }
}