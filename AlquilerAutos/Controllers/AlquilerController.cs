using AlquilerAutos.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlquilerAutos.Controllers
{
    public class AlquilerController : Controller
    {

        private readonly HttpClient _http;

        public AlquilerController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("ApiClient");
        }

        // LISTAR
        public async Task<IActionResult> Index()
        {
            var lista = await _http
                .GetFromJsonAsync<List<Alquiler>>("AlquilerAPI");

            return View(lista);
        }

        // FORM CREATE
        public async Task<IActionResult> Create()
        {
            ViewBag.Clientes = await _http.GetFromJsonAsync<List<Cliente>>("ClienteAPI/obtenerCliente");

            ViewBag.Autos = await _http.GetFromJsonAsync<List<Auto>>("AutoAPI/disponibles");

            return View(new Alquiler());
        }

        // CREATE POST
        [HttpPost]
        public async Task<IActionResult> Create(Alquiler alquiler)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Clientes = await _http.GetFromJsonAsync<List<Cliente>>("ClienteAPI/obtenerCliente");
                ViewBag.Autos = await _http.GetFromJsonAsync<List<Auto>>("AutoAPI/disponibles");
                return View(alquiler);
            }

            var resp = await _http.PostAsJsonAsync("AlquilerAPI", alquiler);

            if (resp.IsSuccessStatusCode)
            {
                TempData["mensaje"] = "Alquiler registrado correctamente";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error al registrar alquiler");

            ViewBag.Clientes = await _http.GetFromJsonAsync<List<Cliente>>("ClienteAPI/obtenerCliente");
            ViewBag.Autos = await _http.GetFromJsonAsync<List<Auto>>("AutoAPI/disponibles");


            return View(alquiler);
        }

        // FINALIZAR
        public async Task<IActionResult> Finalizar(int id)
        {
            var resp = await _http
                .PutAsync($"AlquilerAPI/finalizar/{id}", null);

            if (resp.IsSuccessStatusCode)
                TempData["mensaje"] = "Alquiler finalizado";

            return RedirectToAction(nameof(Index));
        }

        // REPORTE
        public async Task<IActionResult> Reporte()
        {
            var data = await _http
                .GetFromJsonAsync<List<ReporteAlquilerDTO>>("AlquilerAPI/reporte");

            return View(data);
        }





    }
}
