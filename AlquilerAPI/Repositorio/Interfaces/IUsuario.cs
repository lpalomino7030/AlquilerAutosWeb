using AlquilerAPI.Models;

namespace AlquilerAPI.Repositorio.Intefaces
{
    public interface IUsuario
    {
        //CRUD Usuario
        List<Usuarios> ListarUsuario();

        int InsertarUsuario(Usuarios usuario);

        int UpdateUsuario(Usuarios usuario);

        //Consultas especiales
    }
}
