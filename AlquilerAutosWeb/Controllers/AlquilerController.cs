using AlquilerAutosWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;


namespace AlquilerAutosWeb.Controllers
{
    public class AlquileresController : Controller
    {
        public IActionResult Index(string buscar)
        {
            AlquilerDAO dao = new AlquilerDAO();
            List<Alquiler> lista;

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

            ClienteDAO clienteDAO = new ClienteDAO();
            AutoDAO autoDAO = new AutoDAO();

            ViewBag.Clientes = clienteDAO.ListarActivos();
            ViewBag.Autos = autoDAO.ListarDisponibles();

            return View();
        }

        //

        [HttpPost]
        public IActionResult Create(Alquiler a)
        {
            if (HttpContext.Session.GetString("usuario") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            AutoDAO autoDAO = new AutoDAO();
            AlquilerDAO alquilerDAO = new AlquilerDAO();

            // Obtener precio del auto
            Auto auto = autoDAO.ObtenerPrecio(a.AutoId);

            // Calcular días
            int dias = (a.FechaFin - a.FechaInicio).Days;
            if (dias <= 0) dias = 1;

            // Calcular total
            a.Total = dias * auto.PrecioDia;

            // Guardar alquiler
            alquilerDAO.Insertar(a);

            // Marcar auto como alquilado
            autoDAO.MarcarAlquilado(a.AutoId);

            return RedirectToAction("Index");
        }

        //


        public IActionResult Finalizar(int id)
        {
            AlquilerDAO dao = new AlquilerDAO();
            dao.Finalizar(id);

            return RedirectToAction("Index");
        }


        //

        public IActionResult Reporte()
        {
            if (HttpContext.Session.GetString("usuario") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            AlquilerDAO dao = new AlquilerDAO();
            var lista = dao.ReporteAlquileres();

            return View(lista);
        }

        //METODO DEL PDF

        public IActionResult ReportePDF()
        {
            if (HttpContext.Session.GetString("usuario") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            AlquilerDAO dao = new AlquilerDAO();
            var lista = dao.ReporteAlquileres();

            return new ViewAsPdf("ReportePDF", lista)
            {
                FileName = "Reporte_Alquileres.pdf"
            };
        }


    }
}
