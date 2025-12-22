using AlquilerAutos.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlquilerAutos.Controllers
{
    public class AutosController : Controller
    {
        private readonly HttpClient _http;

        public AutosController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("ApiClient");
        }

        // LISTADO
        public async Task<IActionResult> Index(string? buscar)
        {
            List<Auto> autos;

            if (!string.IsNullOrEmpty(buscar))
            {
                autos = await _http
                    .GetFromJsonAsync<List<Auto>>($"AutoAPI/buscar/{buscar}");
                ViewBag.Buscar = buscar;
            }
            else
            {
                autos = await _http
                    .GetFromJsonAsync<List<Auto>>($"AutoAPI/getAutos");
            }

            return View(autos);
        }

        // FORM CREATE
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        public async Task<IActionResult> Create(Auto auto)
        {
            if (!ModelState.IsValid)
                return View(auto);

            var response = await _http.PostAsJsonAsync("AutoAPI", auto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al registrar el auto");
            return View(auto);
        }

        // FORM EDIT
        public async Task<IActionResult> Edit(int id)
        {
            var auto = await _http.GetFromJsonAsync<Auto>($"AutoAPI/{id}");

            if (auto == null)
                return NotFound();

            return View(auto);
        }

        // EDIT POST
        [HttpPost]
        public async Task<IActionResult> Edit(Auto auto)
        {
            if (!ModelState.IsValid)
                return View(auto);

            var response = await _http.PutAsJsonAsync("AutoAPI", auto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "No se pudo actualizar");
            return View(auto);
        }

        // DELETE
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _http.PutAsync($"AutoAPI/{id}", null);

            return RedirectToAction(nameof(Index));
        }









    }
}
