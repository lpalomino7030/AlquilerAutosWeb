using System.ComponentModel.DataAnnotations;

namespace AlquilerAPI.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio")]
        public string DNI { get; set; }

        [Required(ErrorMessage = "Los nombres son obligatorios")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios")]
        public string Apellidos { get; set; }

        public string Telefono { get; set; }

        [EmailAddress(ErrorMessage = "Email no válido")]
        public string Email { get; set; }

        public bool Estado { get; set; }
    }
}
