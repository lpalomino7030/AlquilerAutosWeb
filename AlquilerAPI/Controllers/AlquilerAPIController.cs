using AlquilerAPI.Repositorio.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace AlquilerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlquilerAPIController : Controller
    {
        private readonly IAlquiler _alquiler;
        private readonly IAuto _auto;
        private readonly ICliente _cliente;

        public AlquilerAPIController(IAlquiler alquiler, IAuto auto, ICliente cliente)
        {
            _alquiler = alquiler;
            _auto = auto;
            _cliente = cliente;
        }

        // http gets
        [HttpGet("getAlquiler")]



        //http post





        public IActionResult Index()
        {
            return View();
        }









    }
}
