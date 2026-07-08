using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using UniGymFitness.Data;
using UniGymFitness.Models;

namespace UniGymFitness.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public AccountController(AppDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string Email, string Senha)
        {
            Email = Email?.Trim() ?? string.Empty;
            Senha = Senha ?? string.Empty;

            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Email == Email);

            if (usuario == null)
            {
                ViewBag.Erro = "E-mail ou senha inválidos.";
                return View();
            }

            var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Senha, Senha);

            if (resultado == PasswordVerificationResult.Failed)
            {
                ViewBag.Erro = "E-mail ou senha inválidos.";
                return View();
            }

            HttpContext.Session.SetString("NomeUsuario", usuario.Nome);
            HttpContext.Session.SetInt32("IdUsuario", usuario.Id);
            HttpContext.Session.SetString("TipoUsuario", usuario.TipoUsuario);

            if (usuario.TipoUsuario == "Administrador")
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return RedirectToAction("Index", "Aluno");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastro(Usuario usuario, string ConfirmarSenha)
        {
            usuario.Nome = usuario.Nome?.Trim() ?? string.Empty;
            usuario.Email = usuario.Email?.Trim() ?? string.Empty;
            usuario.Telefone = usuario.Telefone?.Trim() ?? string.Empty;
            usuario.Plano = usuario.Plano?.Trim() ?? string.Empty;
            usuario.TipoUsuario = "Aluno";

            if (usuario.Senha != ConfirmarSenha)
            {
                ModelState.AddModelError("Senha", "As senhas não conferem.");
            }

            if (_context.Usuarios.Any(u => u.Email == usuario.Email))
            {
                ModelState.AddModelError("Email", "Já existe uma conta cadastrada com este e-mail.");
            }

            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            usuario.Senha = _passwordHasher.HashPassword(usuario, usuario.Senha);

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            TempData["Sucesso"] = "Cadastro realizado com sucesso. Faça login para continuar.";

            return RedirectToAction("Login");
        }
    }
}
