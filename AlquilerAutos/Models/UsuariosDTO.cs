namespace AlquilerAutos.Models
{
    public class UsuariosDTO
    {
        public int Id { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }
}
