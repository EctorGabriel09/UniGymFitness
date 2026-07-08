using Microsoft.AspNetCore.Mvc;
using UniGymFitness.Data;
using UniGymFitness.Models;

namespace UniGymFitness.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
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

            var produtos = _context.Produtos
                .OrderBy(p => p.Nome)
                .ToList();

            return View(produtos);
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
        public IActionResult Criar(Produto produto)
        {
            if (!UsuarioEhAdministrador())
            {
                return RedirectToAction("Login", "Account");
            }

            produto.Nome = produto.Nome?.Trim() ?? string.Empty;
            produto.Descricao = produto.Descricao?.Trim() ?? string.Empty;

            if (!ModelState.IsValid)
            {
                return View(produto);
            }

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            TempData["Sucesso"] = "Produto cadastrado com sucesso!";

            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            if (!UsuarioEhAdministrador())
            {
                return RedirectToAction("Login", "Account");
            }

            var produto = _context.Produtos.Find(id);

            if (produto == null)
                return NotFound();

            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Produto produto)
        {
            if (!UsuarioEhAdministrador())
            {
                return RedirectToAction("Login", "Account");
            }

            produto.Nome = produto.Nome?.Trim() ?? string.Empty;
            produto.Descricao = produto.Descricao?.Trim() ?? string.Empty;

            if (ModelState.IsValid)
            {
                _context.Produtos.Update(produto);
                _context.SaveChanges();

                TempData["Sucesso"] = "Produto atualizado com sucesso!";

                return RedirectToAction("Index");
            }

            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Excluir(int id)
        {
            if (!UsuarioEhAdministrador())
            {
                return RedirectToAction("Login", "Account");
            }

            var produto = _context.Produtos.Find(id);

            if (produto == null)
                return NotFound();

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            TempData["Sucesso"] = "Produto excluído com sucesso!";

            return RedirectToAction("Index");
        }
    }
}
