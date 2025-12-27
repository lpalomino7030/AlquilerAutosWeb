using AlquilerAPI.Models;
using AlquilerAPI.Repositorio.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace AlquilerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlquilerAPIController : ControllerBase
    {
        private readonly IAlquiler _alquiler;
        private readonly IAuto _auto;
        private readonly ICliente _cliente;
        private readonly IReporteAlquiler _reporte;
        public AlquilerAPIController(  IAlquiler alquiler, IAuto auto, ICliente cliente, IReporteAlquiler reporte)
        {
            _alquiler = alquiler;
            _auto = auto;
            _cliente = cliente;
            _reporte = reporte;
        }

        // ================================
        // GET: api/alquiler
        // ================================
        [HttpGet]
        public IActionResult GetAlquileres()
        {
            var lista = _alquiler.ListarAlquiler();
            return Ok(lista);
        }

        // ================================
        // GET: api/alquiler/5
        // ================================
        [HttpGet("{id}")]
        public IActionResult GetAlquilerPorId(int id)
        {
            var alquiler = _alquiler.ObtenerPorId(id);

            if (alquiler == null)
                return NotFound("Alquiler no encontrado");

            return Ok(alquiler);
        }

        // ================================
        // GET: api/alquiler/activos
        // ================================
        [HttpGet("activos")]
        public IActionResult AlquileresActivos()
        {
            int total = _alquiler.AlquileresActivos();
            return Ok(total);
        }

        // ================================
        // GET: api/alquiler/por-mes
        // ================================
        [HttpGet("por-mes")]
        public IActionResult AlquileresPorMes()
        {
            var datos = _alquiler.ObtenerAlquileresPorMes();
            return Ok(datos);
        }

        // ================================
        // GET: api/alquiler/reporte
        // ================================
        [HttpGet("reporte")]
        public IActionResult ReporteAlquileres()
        {
            var lista = _reporte.ReporteAlquileres();

            if (lista == null || lista.Count == 0)
                return NotFound("No existen datos para el reporte");

            return Ok(lista);
        }

        // ================================
        // POST: api/alquiler
        // ================================
        [HttpPost]
        public IActionResult CrearAlquiler([FromBody] Alquiler alquiler)
        {
            if (alquiler == null)
                return BadRequest("Datos inválidos");

            if (alquiler.FechaInicio >= alquiler.FechaFin)
                return BadRequest("La fecha de inicio debe ser menor a la fecha fin");

            int resultado = _alquiler.InsertarAlquiler(alquiler);

            if (resultado <= 0)
                return StatusCode(500, "Error al registrar el alquiler");

            return Ok(new { mensaje = "Alquiler registrado correctamente" });
        }

        // ================================
        // PUT: api/alquiler/finalizar/5
        // ================================
        [HttpPut("finalizar/{id}")]
        public IActionResult FinalizarAlquiler(int id)
        {
            int resultado = _alquiler.Finalizar(id);

            if (resultado == 0)
                return NotFound("No se pudo finalizar el alquiler");

            return Ok(new { mensaje = "Alquiler finalizado correctamente" });
        }



    }
}
