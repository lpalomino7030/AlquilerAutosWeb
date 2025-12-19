namespace AlquilerAPI.Models
{
    public class Auto
    {
        public int Id { get; set; }
        public string Placa { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Anio { get; set; }
        public decimal PrecioDia { get; set; }
        public string EstadoAuto { get; set; } = string.Empty; // Disponible / Alquilado
        public bool Estado { get; set; }
       

    }
}
