using AlquilerAPI.Repositorio.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace AlquilerAPI.Controllers
{
    public class ClienteAPIController : Controller
    {
        private readonly ICliente _cliente;

        public ClienteAPIController(ICliente cliente)
        {
            _cliente = cliente;
        }



        public IActionResult Index()
        {
            return View();
        }
    }
}
