using Microsoft.AspNetCore.Mvc;

namespace AlquilerAutos.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
