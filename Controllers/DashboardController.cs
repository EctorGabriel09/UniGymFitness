using Microsoft.AspNetCore.Mvc;

namespace UniGymFitness.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}