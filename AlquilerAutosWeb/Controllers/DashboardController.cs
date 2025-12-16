using AlquilerAutosWeb.Models;
using Microsoft.AspNetCore.Mvc;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("usuario") == null)
        {
            return RedirectToAction("Index", "Login");
        }

        AutoDAO autoDAO = new AutoDAO();
        AlquilerDAO alquilerDAO = new AlquilerDAO();

        var estadoAutos = autoDAO.ObtenerEstadoAutos();
        var alquileresPorMes = alquilerDAO.ObtenerAlquileresPorMes();

        ViewBag.EstadoAutos = estadoAutos;
        ViewBag.AlquileresPorMes = alquileresPorMes;

        return View();
    }
}
