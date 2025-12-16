using System.ComponentModel.DataAnnotations;

namespace AlquilerAutosWeb.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Usuario { get; set; }

        [Required]
        public string Clave { get; set; }
    }
}
