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
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost]
        public IActionResult Editar(Usuario usuario)
        {
            if (!UsuarioEhAdministrador())
            {
                return RedirectToAction("Login", "Account");
            }

            var usuarioBanco = _context.Usuarios.Find(usuario.Id);

            if (usuarioBanco == null)
            {
                return NotFound();
            }

            // Atualiza apenas os campos permitidos
            usuarioBanco.Nome = usuario.Nome;
            usuarioBanco.Email = usuario.Email;
            usuarioBanco.Telefone = usuario.Telefone;
            usuarioBanco.Plano = usuario.Plano;

            _context.SaveChanges();

            TempData["Sucesso"] = "Usuário atualizado com sucesso!";

            return RedirectToAction("Index");
        }

        public IActionResult Excluir(int id)
        {
            if (!UsuarioEhAdministrador())
            {
                return RedirectToAction("Login", "Account");
            }

            var usuario = _context.Usuarios.Find(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            TempData["Sucesso"] = "Usuário excluído com sucesso!";

            return RedirectToAction("Index");
        }
    }
}