
namespace AlquilerAPI.Models
{
    public class Alquiler
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int AutoId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Total { get; set; }
        public string EstadoAlquiler { get; set; } // Activo / Finalizado
        public bool Estado { get; set; }

        public string ClienteNombre { get; set; }
        public string AutoNombre { get; set; }

    }
}
