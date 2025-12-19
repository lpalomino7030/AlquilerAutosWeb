using AlquilerAPI.Modelo;
using AlquilerAPI.Repositorio.Intefaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AlquilerAPI.Repositorio.DAO
{
    public class ReporteAlquilerDAO : IReporteAlquiler
    {

        private readonly string cadena = string.Empty;


        public ReporteAlquilerDAO() {
            cadena = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build()
        .GetConnectionString("cn");
        }

        public List<ReporteAlquilerDTO> ReporteAlquileres()
        {
            List<ReporteAlquilerDTO> lista = new List<ReporteAlquilerDTO>();

            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_reporte_alquileres", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var item = new ReporteAlquilerDTO
                        {
                            Cliente = dr.GetString(0),
                            Placa = dr.GetString(1),
                            Auto = dr.GetString(2),
                            FechaInicio = dr.GetDateTime(3),
                            FechaFin = dr.GetDateTime(4),
                            Total = dr.GetDecimal(5),
                            EstadoAlquiler = dr.GetString(6)
                        };

                        lista.Add(item);
                    }
                }
            }

            return lista;
        }
    }
}
