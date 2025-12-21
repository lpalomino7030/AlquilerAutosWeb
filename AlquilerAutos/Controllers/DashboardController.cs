using Microsoft.AspNetCore.Mvc;

namespace AlquilerAutos.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
