using AlquilerAPI.Repositorio.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace AlquilerAPI.Controllers
{
    public class AutoAPIController : Controller
    {
        private readonly IAuto _auto;


        public AutoAPIController(IAuto auto) {
            _auto = auto;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
