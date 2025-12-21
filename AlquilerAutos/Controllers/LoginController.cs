using Microsoft.AspNetCore.Mvc;

namespace AlquilerAutos.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
