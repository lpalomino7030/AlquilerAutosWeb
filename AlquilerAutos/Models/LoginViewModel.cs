using System.ComponentModel.DataAnnotations;

namespace AlquilerAutos.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Usuario { get; set; }

        [Required]
        public string Clave { get; set; }
    }
}
