using AlquilerAPI.Models;
using AlquilerAPI.Repositorio.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace AlquilerAPI.Controllers
{
    public class AutoAPIController : ControllerBase
    {
        private readonly IAuto _auto;


        public AutoAPIController(IAuto auto) {
            _auto = auto;
        }

        // GET: api/AutoAPI
        [HttpGet]
        public IActionResult ListarTodo()
        {
            try
            {
                var lista = _auto.ListarAuto();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno: " + ex.Message);
            }
        }

        // GET: api/AutoAPI/disponibles
        [HttpGet("disponibles")]
        public IActionResult ListarDisponibles()
        {
            var lista = _auto.ListarAutoDisponible();
            return Ok(lista);
        }

        // GET: api/AutoAPI/5
        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            var auto = _auto.ObtenerIdAuto(id);

            if (auto == null)
            {
                return NotFound($"No se encontró el auto con id {id}");
            }

            return Ok(auto);
        }

        // POST: api/AutoAPI
        [HttpPost]
        public IActionResult Guardar([FromBody] Auto auto)
        {
            if (auto == null) return BadRequest("Datos inválidos");

            var resultado = _auto.InsertarAuto(auto);

            if (resultado > 0)
            {
                // Retorna 201 Created
                return CreatedAtAction(nameof(ObtenerPorId), new { id = auto.Id }, auto); // Nota: auto.Id debe actualizarse si es identity
            }

            return BadRequest("No se pudo insertar el auto");
        }

        // PUT: api/AutoAPI/5
        [HttpPut]
        public IActionResult Actualizar([FromBody] Auto auto)
        {
            if (auto == null) return BadRequest();

            var resultado = _auto.UpdatearAuto(auto);

            if (resultado > 0)
            {
                return Ok(new { mensaje = "Auto actualizado correctamente" });
            }

            return NotFound("No se pudo actualizar, verifique el ID");
        }

        // DELETE: api/AutoAPI/5
        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            var resultado = _auto.EliminarAuto(id);

            if (resultado > 0)
            {
                return Ok(new { mensaje = "Auto eliminado correctamente" });
            }

            return NotFound($"No se encontró el auto con id {id} para eliminar");
        }

        // GET: api/AutoAPI/buscar/toyota
        [HttpGet("buscar/{texto}")]
        public IActionResult Buscar(string texto)
        {
            var resultados = _auto.Buscar(texto);
            return Ok(resultados);
        }

        // PATCH: api/AutoAPI/marcar-alquilado/5
        [HttpPatch("marcar-alquilado/{id}")]
        public IActionResult MarcarAlquilado(int id)
        {
            var resultado = _auto.MarcarAlquilado(id);
            if (resultado > 0) return Ok("Auto marcado como alquilado");
            return BadRequest("No se pudo cambiar el estado");
        }

        // PATCH: api/AutoAPI/marcar-disponible/5
        [HttpPatch("marcar-disponible/{id}")]
        public IActionResult MarcarDisponible(int id)
        {
            var resultado = _auto.MarcarDisponible(id);
            if (resultado > 0) return Ok("Auto marcado como disponible");
            return BadRequest("No se pudo cambiar el estado");
        }

        // GET: api/AutoAPI/estadisticas
        [HttpGet("estadisticas")]
        public IActionResult ObtenerEstadisticas()
        {
            var total = _auto.TotalAutos();
            var disponibles = _auto.AutoDisponibles();
            var desglose = _auto.ObtenerEstadoAutos();

            return Ok(new
            {
                Total = total,
                TotalDisponibles = disponibles,
                DetalleEstados = desglose
            });
        }

        // GET: api/AutoAPI/precio/5
        [HttpGet("precio/{id}")]
        public IActionResult ObtenerPrecio(int id)
        {
            var auto = _auto.ObtenerPrecioAuto(id);
            if (auto == null) return NotFound();

            return Ok(new { Id = auto.Id, Precio = auto.PrecioDia });
        }

    }
}
