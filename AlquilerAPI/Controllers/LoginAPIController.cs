using AlquilerAPI.Repositorio.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace AlquilerAPI.Controllers
{
    public class LoginAPIController : ControllerBase
    {
        private readonly ILogin _login;

        public LoginAPIController(ILogin login)
        {
            _login = login;
        }



    }
}
