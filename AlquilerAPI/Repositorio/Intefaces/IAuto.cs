using AlquilerAPI.Models;

namespace AlquilerAPI.Repositorio.Intefaces
{
    public interface IAuto
    {

        //CRUD Auto
        List<Auto> ListarAuto();
        List<Auto> ListarAutoDisponible();
        int InsertarAuto(Auto auto);
        //actualizar
        int UpdatearAuto(Auto auto);
        //buscar
        Auto ObtenerIdAuto(int id);
        //eliminar marcar
        int EliminarAuto(int id);
        int MarcarAlquilado(int id);
        int MarcarDisponible(int id);
        int TotalAutos();
        int AutoDisponibles();


        Auto ObtenerPrecioAuto(int id);





    }
}
