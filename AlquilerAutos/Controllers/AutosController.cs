using Microsoft.AspNetCore.Mvc;

namespace AlquilerAutos.Controllers
{
    public class AutosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
