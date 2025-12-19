using AlquilerAPI.Models;

namespace AlquilerAPI.Repositorio.Intefaces
{
    public interface IAuto
    {

        //CRUD Auto
        IEnumerable<Auto> ListarAuto();
    }
}
