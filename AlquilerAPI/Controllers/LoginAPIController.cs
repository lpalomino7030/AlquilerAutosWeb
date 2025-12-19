using AlquilerAPI.Repositorio.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace AlquilerAPI.Controllers
{
    public class LoginAPIController : Controller
    {
        private readonly ILogin _login;

        public LoginAPIController(ILogin login)
        {
            _login = login;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
