using Microsoft.AspNetCore.Mvc;

namespace UniGymFitness.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Cadastro()
        {
            return View();
        }
    }
}