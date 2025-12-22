using AlquilerAutos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace AlquilerAutos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;



        public LoginController(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _config = configuration;
        }



        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
           

            if (!ModelState.IsValid)
                return View(model);

            var client = _httpClientFactory.CreateClient("ApiClient");

            var json = JsonSerializer.Serialize(new
            {
                Usuario = model.Usuario,
                Password  = model.Clave
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("auth/validar", content);

            if (response.IsSuccessStatusCode)
            {
                var resultJson = await response.Content.ReadAsStringAsync();
                var usuario = JsonSerializer.Deserialize<UsuariosDTO>(resultJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            
                HttpContext.Session.SetString("Usuario", usuario.Usuario);
                HttpContext.Session.SetString("Rol", usuario.Rol);

                return RedirectToAction("Index", "Dashboard");
            }

            ViewBag.Error = "Usuario o contraseña incorrectos";
            return View(model);
        }



        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
