using Microsoft.AspNetCore.Mvc;
using UniGymFitness.Data;

namespace UniGymFitness.Controllers
{
    public class AlunoController : Controller
    {
        private readonly AppDbContext _context;

        public AlunoController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");

            if (idUsuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var aluno = _context.Usuarios.Find(idUsuario);

            if (aluno == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(aluno);
        }
    }
}