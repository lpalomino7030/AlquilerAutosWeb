using AlquilerAutosWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlquilerAutosWeb.Controllers
{
    public class ClientesController : Controller
    {
        public IActionResult Index(string buscar)
        {
            ClienteDAO dao = new ClienteDAO();
            List<Cliente> lista;

            if (string.IsNullOrEmpty(buscar))
            {
                lista = dao.Listar();
            }
            else
            {
                lista = dao.Buscar(buscar);
            }

            ViewBag.Buscar = buscar;
            return View(lista);
        }

        //


        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("usuario") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(Cliente c)
        {
            if (HttpContext.Session.GetString("usuario") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (!ModelState.IsValid)
            {
                return View(c);
            }

            ClienteDAO dao = new ClienteDAO();
            dao.Insertar(c);

            TempData["mensaje"] = "Cliente registrado correctamente";

            return RedirectToAction("Index");
        }

        //

        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("usuario") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ClienteDAO dao = new ClienteDAO();
            Cliente c = dao.ObtenerPorId(id);

            return View(c);
        }

        [HttpPost]
        public IActionResult Edit(Cliente c)
        {
            if (HttpContext.Session.GetString("usuario") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ClienteDAO dao = new ClienteDAO();
            dao.Actualizar(c);

            return RedirectToAction("Index");
        }

        //
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("usuario") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ClienteDAO dao = new ClienteDAO();
            dao.Eliminar(id);

            return RedirectToAction("Index");
        }


    }
}
