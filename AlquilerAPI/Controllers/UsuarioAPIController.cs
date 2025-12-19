using AlquilerAPI.Repositorio.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace AlquilerAPI.Controllers
{
    public class UsuarioAPIController : Controller
    {

        private readonly IUsuario _usuario;

        public UsuarioAPIController(IUsuario usuario)
        {
            _usuario = usuario;
        }




        public IActionResult Index()
        {
            return View();
        }
    }
}
