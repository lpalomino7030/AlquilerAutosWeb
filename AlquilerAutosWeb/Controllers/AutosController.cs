using AlquilerAutosWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlquilerAutosWeb.Controllers
{
    public class AutosController : Controller
    {
        public IActionResult Index(string buscar)
        {
            AutoDAO dao = new AutoDAO();
            List<Auto> lista;

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
        public IActionResult Create(Auto a)
        {
            if (HttpContext.Session.GetString("usuario") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            AutoDAO dao = new AutoDAO();
            dao.Insertar(a);

            return RedirectToAction("Index");
        }

        //

        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("usuario") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            AutoDAO dao = new AutoDAO();
            Auto a = dao.ObtenerPorId(id);

            return View(a);
        }

        [HttpPost]
        public IActionResult Edit(Auto a)
        {
            if (HttpContext.Session.GetString("usuario") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            AutoDAO dao = new AutoDAO();
            dao.Actualizar(a);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("usuario") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            AutoDAO dao = new AutoDAO();
            dao.Eliminar(id);

            return RedirectToAction("Index");
        }


    }
}
