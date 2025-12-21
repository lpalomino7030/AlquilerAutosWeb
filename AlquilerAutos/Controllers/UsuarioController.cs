using Microsoft.AspNetCore.Mvc;
using AlquilerAutos.Models;

namespace AlquilerAutos.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
