using System.ComponentModel.DataAnnotations;

namespace AlquilerAPI.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio")]
        public string DNI { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los nombres son obligatorios")]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los apellidos son obligatorios")]
        public string Apellidos { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Email no válido")]
        public string Email { get; set; } = string.Empty;

        public bool Estado { get; set; }
    }
}
