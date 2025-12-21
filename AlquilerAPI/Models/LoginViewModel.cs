using System.ComponentModel.DataAnnotations;

namespace AlquilerAPI.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Usuario { get; set; } = string.Empty;

        [Required]
        public string Clave { get; set; } = string.Empty;
    }
}
