using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AlquilerAutos.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public DashboardController(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {

            if (HttpContext.Session.GetString("Usuario") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var client = _httpClientFactory.CreateClient();
            var baseUrl = _configuration["ApiSettings:BaseUrl"];


            var respAutos = await client.GetAsync($"{baseUrl}AutoAPI/estadisticas");

            if (respAutos.IsSuccessStatusCode)
            {
                var json = await respAutos.Content.ReadAsStringAsync();
                var data = JsonDocument.Parse(json);

                var detalleEstados = data
                    .RootElement
                    .GetProperty("detalleEstados");

                var estadoAutos = new Dictionary<string, int>();

                foreach (var item in detalleEstados.EnumerateObject())
                {
                    estadoAutos[item.Name] = item.Value.GetInt32();
                }

                ViewBag.EstadoAutos = estadoAutos;
            }
            else
            {
                ViewBag.EstadoAutos = new Dictionary<string, int>();
            }
            var respMes = await client.GetAsync($"{baseUrl}AlquilerAPI/por-mes");

            if (respMes.IsSuccessStatusCode)
            {
                var json = await respMes.Content.ReadAsStringAsync();
                var alquileresMes =
                    JsonSerializer.Deserialize<Dictionary<string, int>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                ViewBag.AlquileresPorMes = alquileresMes;
            }
            else
            {
                ViewBag.AlquileresPorMes = new Dictionary<string, int>();
            }

            return View();
        }


        //public IActionResult Index()
        //{

        //    ViewBag.EstadoAutos = new Dictionary<string, int>
        //    {
        //        { "Disponible", 6 },
        //        { "Alquilado", 3 },
        //        { "Mantenimiento", 1 }
        //    };

        //    ViewBag.AlquileresPorMes = new Dictionary<string, int>
        //    {
        //        { "Enero", 4 },
        //        { "Febrero", 6 },
        //        { "Marzo", 2 }
        //    };

        //    return View();
        //}

    }
}
