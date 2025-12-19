using System.ComponentModel.DataAnnotations;

namespace AlquilerAPI.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Usuario { get; set; }

        [Required]
        public string Clave { get; set; }
    }
}
