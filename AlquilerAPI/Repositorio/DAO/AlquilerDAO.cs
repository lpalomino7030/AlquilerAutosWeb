using AlquilerAPI.Models;
using AlquilerAPI.Repositorio.Intefaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AlquilerAPI.Repositorio.DAO
{
    public class AlquilerDAO : IAlquiler
    {
        private readonly string cadena = string.Empty;

        public AlquilerDAO()
        {
            cadena = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("cn");
        }


        public Dictionary<string, int> ObtenerAlquileresPorMes()
        {
            Dictionary<string, int> datos = new Dictionary<string, int>();

            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_alquileres_por_mes", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int mes = dr.GetInt32(0);
                        int total = dr.GetInt32(1);  

                        string nombreMes = new DateTime(
                            DateTime.Now.Year, mes, 1
                        ).ToString("MMMM");

                        datos[nombreMes] = total;
                    }
                }
            }
            return datos;
        }

        public int AlquileresActivos()
        {
            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_alquileres_activos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int Finalizar(int id)
        {
            int rpta = 0;
            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_finalizar_alquiler", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                rpta = cmd.ExecuteNonQuery();
            }

            return rpta;
        }

        public Alquiler ObtenerPorId(int id)
        {
            Alquiler a = null;

            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_obtener_alquiler_id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        a = new Alquiler
                        {
                            Id = dr.GetInt32(0),
                            AutoId = dr.GetInt32(1),
                            EstadoAlquiler = dr.GetString(2),
                        };
                    }
                }
            }

            return a;
        }

        public int InsertarAlquiler(Alquiler item)
        {
            int rpta = 0;

            using (SqlConnection con = new SqlConnection(cadena))
            {
                con.Open();
                try
                {

                    SqlCommand cmd = new SqlCommand("sp_insertar_alquiler", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ClienteId", item.ClienteId);
                    cmd.Parameters.AddWithValue("@AutoId", item.AutoId);
                    cmd.Parameters.AddWithValue("@FechaInicio", item.FechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", item.FechaFin);
                    cmd.Parameters.AddWithValue("@Total", item.Total);
                     rpta = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    rpta = -1;
                }
                finally
                {
                    con.Close();
                }
                
            }
            return rpta;
        }

        public IEnumerable<Alquiler> ListarAlquiler()
        {
            List<Alquiler> lista = new List<Alquiler>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_listar_alquileres_activos", cn);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Alquiler()
                    {
                        Id = dr.GetInt32(0),
                        FechaInicio = dr.GetDateTime(1),
                        FechaFin = dr.GetDateTime(2),
                        Total = dr.GetDecimal(3),
                        Estado = dr.GetBoolean(4),
                        EstadoAlquiler = dr.GetString(5),
                        ClienteNombre = dr.GetString(6),
                        AutoNombre = dr.GetString(7)

                    });

                }



            }

            return lista;
        }
    }
}
