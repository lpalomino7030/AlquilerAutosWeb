using AlquilerAPI.Models;
using AlquilerAPI.Repositorio.Intefaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AlquilerAPI.Repositorio.DAO
{
    public class AutoDAO : IAuto
    {
        private readonly string cadena = string.Empty;

        public AutoDAO() {

            cadena = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build()
    .GetConnectionString("cn");
        }

        public List<Auto> ListarAuto()
        {
            List<Auto> lista = new List<Auto>();

            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_listar_autos_activos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Auto
                    {
                        Id = dr.GetInt32(0),
                        Placa = dr.GetString(1),
                        Marca = dr.GetString(2),
                        Modelo = dr.GetString(3),
                        Anio = dr.GetInt32(4),
                        PrecioDia = dr.GetDecimal(5),
                        EstadoAuto = dr.GetString(6),
                        Estado = dr.GetBoolean(7)
                    });
                }
            }

            return lista;
        }

        public int InsertarAuto(Auto a)
        {
            var rpta = 0;

            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_insertar_auto", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@placa", a.Placa);
                cmd.Parameters.AddWithValue("@marca", a.Marca);
                cmd.Parameters.AddWithValue("@modelo", a.Modelo);
                cmd.Parameters.AddWithValue("@anio", a.Anio);
                cmd.Parameters.AddWithValue("@precio", a.PrecioDia);

                con.Open();
                rpta = cmd.ExecuteNonQuery();

                return rpta;
            }
        }


        public Auto ObtenerIdAuto(int id)
        {
            Auto a = null;

            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_obtener_auto_por_id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    a = new Auto
                    {
                        Id = dr.GetInt32(0),
                        Placa = dr.GetString(1),
                        Marca = dr.GetString(2),
                        Modelo = dr.GetString(3),
                        Anio = dr.GetInt32(4),
                        PrecioDia = dr.GetDecimal(5),
                        EstadoAuto = dr.GetString(6),
                        Estado = dr.GetBoolean(7)
                    };
                }
            }

            return a;
        }


        public int UpdatearAuto(Auto a)
        {
            var rpta = 0;
            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_actualizar_auto", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", a.Id);
                cmd.Parameters.AddWithValue("@placa", a.Placa);
                cmd.Parameters.AddWithValue("@marca", a.Marca);
                cmd.Parameters.AddWithValue("@modelo", a.Modelo);
                cmd.Parameters.AddWithValue("@anio", a.Anio);
                cmd.Parameters.AddWithValue("@precio", a.PrecioDia);

                con.Open();
                return rpta = cmd.ExecuteNonQuery();
            }
        }



        public int EliminarAuto(int id)
        {
            var rpta = 0;
            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_autos_eliminar", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                return rpta = cmd.ExecuteNonQuery();
            }
        }

        public List<Auto> ListarAutoDisponible()
        {
            List<Auto> lista = new List<Auto>();

            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_listar_autos_disponibles", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Auto
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("Id")),
                        Placa = dr.GetString(dr.GetOrdinal("Placa")),
                        Marca = dr.GetString(dr.GetOrdinal("Marca")),
                        Modelo = dr.GetString(dr.GetOrdinal("Modelo")),
                        PrecioDia = dr.GetDecimal(dr.GetOrdinal("PrecioDia"))
                    });
                }
            }

            return lista;
   
        }


        public Auto ObtenerPrecioAuto(int id)
        {
            Auto a = null;

            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_obtener_precio_auto", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    a = new Auto
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("Id")),
                        PrecioDia = dr.GetDecimal(dr.GetOrdinal("PrecioDia"))
                    };
                }
            }

            return a;
        }


        public int MarcarAlquilado(int id)
        {
            int rpta = 0;

            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_autos_marcar_alquilado", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
               return rpta = cmd.ExecuteNonQuery();
            }


        }

        public int MarcarDisponible(int id)
        {
            var rpta = 0;
            using (SqlConnection con =new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_marcar_auto_disponible", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                return rpta = cmd.ExecuteNonQuery();
            }
        }

        public int TotalAutos()
        {
            var total = 0;
            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_total_autos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                return total = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // realizar autos disponibles()

        public int AutoDisponibles()
        {
            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_autos_disponibles", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }




        //metodo que compara autos disponibles

        public Dictionary<int, string> ObtenerEstadoAutos()
        {
            Dictionary<int, string> datos = new Dictionary<int, string>();

            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_obtener_estado_autos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    datos.Add(
                        
                        Convert.ToInt32(dr["Total"]), dr["EstadoAuto"].ToString()
                    );
                }
            }

            return datos;
        }


        public List<Auto> Buscar(string texto)
        {
            List<Auto> lista = new List<Auto>();

            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_buscar_autos", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Texto", texto);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Auto auto = new Auto
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("Id")),
                        Placa = dr.GetString(dr.GetOrdinal("Placa")),
                        Marca = dr.GetString(dr.GetOrdinal("Marca")),
                        Modelo = dr.GetString(dr.GetOrdinal("Modelo")),
                        Anio = dr.GetInt32(dr.GetOrdinal("Anio")),
                        PrecioDia = dr.GetDecimal(dr.GetOrdinal("PrecioDia")),
                        EstadoAuto = dr.GetString(dr.GetOrdinal("EstadoAuto")),
                        Estado = dr.GetBoolean(dr.GetOrdinal("Estado"))
                    };

                    lista.Add(auto);
                }
            }

            return lista;
        }



    }
}
