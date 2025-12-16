using AlquilerAutosWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlquilerAutosWeb.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string usuario, string clave)
        {
            UsuarioDAO dao = new UsuarioDAO();
            var user = dao.ValidarLogin(usuario, clave);

            if (user != null)
            {
                // GUARDAR DATOS EN SESIÓN (CORRECTO)
                HttpContext.Session.SetString("usuario", user.UsuarioNombre);
                HttpContext.Session.SetString("rol", user.Rol);

                return RedirectToAction("Index", "Dashboard");
            }

            ViewBag.Error = "Usuario o contraseña incorrectos";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}
