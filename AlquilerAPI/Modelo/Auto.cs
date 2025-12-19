namespace AlquilerAPI.Models
{
    public class Auto
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }
        public decimal PrecioDia { get; set; }
        public string EstadoAuto { get; set; } // Disponible / Alquilado
        public bool Estado { get; set; }
       

    }
}
