using AlquilerAPI.Models;
using AlquilerAPI.Repositorio.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace AlquilerAPI.Controllers
{
    public class ClienteAPIController : ControllerBase
    {
        private readonly ICliente _cliente;

        public ClienteAPIController(ICliente cliente)
        {
            _cliente = cliente;
        }

        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var lista = _cliente.ListarCliente();
            return Ok(lista);
        }

        [HttpGet("Obtener/{id}")]
        public IActionResult Obtener(int id)
        {
            var obj = _cliente.ObtenerClienteId(id);
            if (obj == null) return NotFound("Cliente no encontrado");
            return Ok(obj);
        }

        [HttpGet("Buscar")]
        public IActionResult Buscar([FromQuery] string texto)
        {
            var lista = _cliente.BuscarCliente(texto);
            return Ok(lista);
        }

        [HttpPost("Guardar")]
        public IActionResult Guardar([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var respuesta = _cliente.InsertarCliente(cliente);
            if (respuesta > 0) return Ok(new { mensaje = "Cliente registrado correctamente" });
            return StatusCode(500, "Error al registrar cliente");
        }

        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var respuesta = _cliente.UpdateCliente(cliente);
            if (respuesta > 0) return Ok(new { mensaje = "Cliente actualizado correctamente" });
            return BadRequest("No se pudo actualizar el cliente");
        }

        [HttpDelete("Eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            var respuesta = _cliente.DeleteCliente(id);
            if (respuesta > 0) return Ok(new { mensaje = "Cliente eliminado" });
            return BadRequest("No se pudo eliminar el cliente");
        }

        [HttpGet("Total")]
        public IActionResult Total()
        {
            return Ok(new { total = _cliente.TotalClientes() });
        }


    }
}
