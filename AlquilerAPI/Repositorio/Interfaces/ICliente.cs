using AlquilerAPI.Models;

namespace AlquilerAPI.Repositorio.Intefaces
{
    public interface ICliente
    {
        //CRUD Cliente
        List<Cliente> ListarCliente();

        int InsertarCliente(Cliente cliente);

        Cliente ObtenerClienteId(int id);

        int UpdateCliente(Cliente cliente);

        int DeleteCliente(int id);

        List<Cliente> ClientesActivos();

        int TotalClientes();

        List<Cliente> BuscarCliente(string texto);






    }
}
