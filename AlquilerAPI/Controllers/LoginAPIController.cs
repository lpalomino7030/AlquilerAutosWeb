using AlquilerAPI.Repositorio.Intefaces;
using AlquilerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlquilerAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class LoginAPIController : ControllerBase
    {
        private readonly ILogin _login;

        public LoginAPIController(ILogin login)
        {
            _login = login;
        }

        [HttpPost("validar")]
        public IActionResult ComprobarUsuario([FromBody] Usuarios usr)
        {
            var user = _login.ValidarLogin(usr.Usuario, usr.Password);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest();

        }


        [HttpPost("login")]
        public IActionResult ValidarLogin([FromBody] Usuarios usuario)
        {

            if (usuario.Usuario == "admin" && usuario.Password == "123")
            {
                return Ok(new
                {
                    Id = 1,
                    Usuario = "admin",
                    Rol = "Administrador",
                    Estado = true
                });
            }


            return Unauthorized("Usuario o contraseña incorrectos");
        }


    }
}
