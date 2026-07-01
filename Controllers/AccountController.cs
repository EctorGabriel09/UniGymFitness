using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using UniGymFitness.Data;
using UniGymFitness.Models;
using System.Linq;

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
        public IActionResult Login(string Email, string Senha)
        {
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
        public IActionResult Cadastro(Usuario usuario)
        {
            usuario.TipoUsuario = "Aluno";
            usuario.Senha = _passwordHasher.HashPassword(usuario, usuario.Senha);

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }
    }
}