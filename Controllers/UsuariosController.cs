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

            var usuarios = _context.Usuarios
                .OrderBy(u => u.Nome)
                .ToList();

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
        [ValidateAntiForgeryToken]
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
            usuarioBanco.Nome = usuario.Nome?.Trim() ?? string.Empty;
            usuarioBanco.Email = usuario.Email?.Trim() ?? string.Empty;
            usuarioBanco.Telefone = usuario.Telefone?.Trim() ?? string.Empty;
            usuarioBanco.Plano = usuario.Plano?.Trim() ?? string.Empty;

            _context.SaveChanges();

            TempData["Sucesso"] = "Usuário atualizado com sucesso!";

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

            var usuarioLogadoId = HttpContext.Session.GetInt32("IdUsuario");

            if (usuario.Id == usuarioLogadoId)
            {
                TempData["Erro"] = "Você não pode excluir o próprio usuário logado.";
                return RedirectToAction("Index");
            }

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            TempData["Sucesso"] = "Usuário excluído com sucesso!";

            return RedirectToAction("Index");
        }
    }
}
