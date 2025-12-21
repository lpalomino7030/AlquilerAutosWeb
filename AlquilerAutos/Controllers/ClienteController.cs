using AlquilerAutos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;

namespace AlquilerAutos.Controllers
{
    public class ClienteController : Controller
    {

        private readonly HttpClient _http;

        public ClienteController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("ApiCliente");
        }

        // LISTAR + BUSCAR
        public async Task<IActionResult> Index(string buscar)
        {
            IEnumerable<Cliente> lista;

            if (!string.IsNullOrEmpty(buscar))
            {
                lista = await _http.GetFromJsonAsync<List<Cliente>>(
                    $"Buscar?texto={buscar}");
                ViewBag.Buscar = buscar;
            }
            else
            {
                lista = await _http.GetFromJsonAsync<List<Cliente>>("Listar");
            }

            return View(lista);
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            return View(new Cliente());
        }

        // CREATE (POST)
        [HttpPost]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (!ModelState.IsValid)
                return View(cliente);

            var response = await _http.PostAsJsonAsync("Guardar", cliente);

            if (response.IsSuccessStatusCode)
            {
                TempData["mensaje"] = "Cliente registrado correctamente";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error al registrar cliente");
            return View(cliente);
        }

        // EDIT (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _http.GetFromJsonAsync<Cliente>($"Obtener/{id}");
            if (cliente == null) return NotFound();

            return View(cliente);
        }

        // EDIT (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(Cliente cliente)
        {
            if (!ModelState.IsValid)
                return View(cliente);

            var response = await _http.PutAsJsonAsync("Actualizar", cliente);

            if (response.IsSuccessStatusCode)
            {
                TempData["mensaje"] = "Cliente actualizado correctamente";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "No se pudo actualizar");
            return View(cliente);
        }

        // DELETE
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _http.PutAsync($"Eliminar/{id}", null);

            if (response.IsSuccessStatusCode)
                TempData["mensaje"] = "Cliente eliminado correctamente";

            return RedirectToAction(nameof(Index));
        }






    }
}
