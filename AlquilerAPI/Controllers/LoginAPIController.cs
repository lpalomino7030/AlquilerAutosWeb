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

        //[HttpPost("login")]
        //public IActionResult ValidarLogin([FromBody] Usuarios usuario)
        //{
        //    try
        //    {
        //        var user = _login.ValidarLogin(usuario.Usuario, usuario.Password);
        //        if (user != null)
        //        {
        //            return Ok(user);
        //        }
        //        else
        //        {
        //            return Unauthorized("Usuario o contraseña incorrectos");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Error interno: " + ex.Message);
        //    }
        //}

        //[HttpPost("login")]
        //public IActionResult ValidarLogin([FromBody] Usuarios usuario)
        //{
            
        //    if (usuario.Usuario == "admin" && usuario.Password == "123")
        //    {
        //        return Ok(new
        //        {
        //            Id = 1,
        //            Usuario = "admin",
        //            Rol = "Administrador",
        //            Estado = true
        //        });
        //    }
        //    else
        //    {
        //        var user = _login.ValidarLogin(usuario.Usuario, usuario.Password);
        //        if (user != null)
        //        {
        //            return Ok(user);
        //        }
        //        else
        //        {
        //            return Unauthorized("Usuario o contraseña incorrectos");
        //        }
        //    }

           // return Unauthorized("Usuario o contraseña incorrectos");
        //}

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
