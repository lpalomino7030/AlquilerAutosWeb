using Microsoft.AspNetCore.Mvc;

namespace AlquilerAutos.Controllers
{
    public class AlquilerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
