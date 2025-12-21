using AlquilerAPI.Models;

namespace AlquilerAPI.Repositorio.Intefaces
{
    public interface ILogin
    {
        Usuarios? ValidarLogin(string usuario, string clave);
    }
}
