using AlquilerAPI.Models;

namespace AlquilerAPI.Repositorio.Intefaces
{
    public interface IAlquiler
    {
        //CRUD Alquiler
        IEnumerable<Alquiler> ListarAlquiler();

        //Consultas especiales
        int InsertarAlquiler(Alquiler alquiler);

        Alquiler ObtenerPorId(int id);

        int Finalizar(int id);

        int AlquileresActivos();


        Dictionary<string, int> ObtenerAlquileresPorMes();
    }
}
