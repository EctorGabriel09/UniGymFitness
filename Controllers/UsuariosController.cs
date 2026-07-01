using Microsoft.AspNetCore.Mvc;
using UniGymFitness.Data;
using UniGymFitness.Models;

namespace UniGymFitness.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
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

            var usuarios = _context.Usuarios.ToList();
            return View(usuarios);
        }

        public IActionResult Editar(int id)
        {
            if (!UsuarioEhAdministrador())
            {
                return RedirectToAction("Login", "Account");
            }

            var usuario = _context.Usuarios.Find(id);

            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        [HttpPost]
        public IActionResult Editar(Usuario usuario)
        {
            if (!UsuarioEhAdministrador())
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                _context.Usuarios.Update(usuario);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        public IActionResult Excluir(int id)
        {
            if (!UsuarioEhAdministrador())
            {
                return RedirectToAction("Login", "Account");
            }

            var usuario = _context.Usuarios.Find(id);

            if (usuario == null)
                return NotFound();

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}