namespace AlquilerAutos.Models
{
    public class ReporteAlquilerDTO
    {
        public string Cliente { get; set; } = string.Empty;
        public string Placa { get; set; } = string.Empty;
        public string Auto { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Total { get; set; }
        public string EstadoAlquiler { get; set; } = string.Empty;
    }
}
