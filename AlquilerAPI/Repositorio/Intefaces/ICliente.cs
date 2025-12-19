using AlquilerAPI.Models;

namespace AlquilerAPI.Repositorio.Intefaces
{
    public interface ICliente
    {
        //CRUD Cliente
        IEnumerable<Cliente> ListarCliente();

    }
}
