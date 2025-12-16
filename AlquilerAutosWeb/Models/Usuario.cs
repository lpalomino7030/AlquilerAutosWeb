namespace AlquilerAutosWeb.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string UsuarioNombre { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
        public bool Estado { get; set; }
    }
}
