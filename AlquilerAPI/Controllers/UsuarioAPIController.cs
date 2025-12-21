using AlquilerAPI.Repositorio.Intefaces;
using Microsoft.AspNetCore.Mvc;
using AlquilerAPI.Models;

namespace AlquilerAPI.Controllers
{
    public class UsuarioAPIController : ControllerBase
    {

        private readonly IUsuario _usuario;

        public UsuarioAPIController(IUsuario usuario)
        {
            _usuario = usuario;
        }

        [HttpGet("obtenerusuarios")]
        public IActionResult ListarUsuarios()
        {
            var usr = _usuario.ListarUsuario();

            if (usr == null)
            {
                return NotFound($"No se encontro el usuario");
            }

            return Ok(usr);

        }


        [HttpPost("agregarusuario")]
        public IActionResult InsertarUsuario([FromBody] Usuarios usuario)
        {
            if (usuario == null) return BadRequest("Datos inválidos");
            var resultado = _usuario.InsertarUsuario(usuario);
            if (resultado > 0)
            {
                return Ok("Usuario insertado correctamente");
            }
            else
            {
                return StatusCode(500, "Error al insertar el usuario");
            }
        }


        [HttpPut("actualizarusuario")]
        public IActionResult UpdateUsuario([FromBody] Usuarios usuario)
        {
            if (usuario == null) return BadRequest("Datos inválidos");
            var resultado = _usuario.UpdateUsuario(usuario);
            if (resultado > 0)
            {
                return Ok("Usuario actualizado correctamente");
            }
            else
            {
                return StatusCode(500, "Error al actualizar el usuario");
            }
        }













    }
}
