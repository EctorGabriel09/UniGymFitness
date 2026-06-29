using Microsoft.AspNetCore.Mvc;
using UniGymFitness.Data;
using UniGymFitness.Models;
using System.Linq;

namespace UniGymFitness.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Email, string Senha)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Email == Email && u.Senha == Senha);

            if (usuario == null)
            {
                ViewBag.Erro = "E-mail ou senha inválidos.";
                return View();
            }

            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }
    }
}