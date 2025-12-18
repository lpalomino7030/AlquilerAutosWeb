using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace AlquilerAutosWeb.Models
{
    public class AlquilerDAO
    {
        Conexion cn = new Conexion();

        public IEnumerable<Alquiler> Listar()
        {
            List<Alquiler> lista = new List<Alquiler>();

            using (SqlConnection con = cn.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_listar_empleados", con);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
            
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read()) {
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

        /*

        public List<Alquiler> Listar()
        {
            List<Alquiler> lista = new List<Alquiler>();

            using (SqlConnection con = cn.GetConnection())
            {
                string sql = @"
            SELECT 
                A.Id,
                A.FechaInicio,
                A.FechaFin,
                A.Total,
                A.Estado,
                A.EstadoAlquiler,
                C.Nombres + ' ' + C.Apellidos AS ClienteNombre,
                AU.Placa + ' - ' + AU.Marca + ' ' + AU.Modelo AS AutoNombre
            FROM Alquileres A
            INNER JOIN Clientes C ON A.ClienteId = C.Id
            INNER JOIN Autos AU ON A.AutoId = AU.Id
            WHERE A.Estado = 1
        ";

                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Alquiler
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        FechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                        FechaFin = Convert.ToDateTime(dr["FechaFin"]),
                        Total = Convert.ToDecimal(dr["Total"]),
                        Estado = Convert.ToBoolean(dr["Estado"]),
                        EstadoAlquiler = dr["EstadoAlquiler"].ToString(),
                        ClienteNombre = dr["ClienteNombre"].ToString(),
                        AutoNombre = dr["AutoNombre"].ToString()
                    });
                }
            }

            return lista;
        }
        */


        public void Insertar(Alquiler a)
        {
            using (SqlConnection con = cn.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_insertar_alquiler", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClienteId", a.ClienteId);
                cmd.Parameters.AddWithValue("@AutoId", a.AutoId);
                cmd.Parameters.AddWithValue("@FechaInicio", a.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", a.FechaFin);
                cmd.Parameters.AddWithValue("@Total", a.Total);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /*
        public void Insertar(Alquiler a)
        {
            using (SqlConnection con = cn.GetConnection())
            {
                string sql = @"INSERT INTO Alquileres
                       (ClienteId, AutoId, FechaInicio, FechaFin, Total, EstadoAlquiler, Estado)
                       VALUES
                       (@cliente, @auto, @inicio, @fin, @total, 'Activo', 1)";

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@cliente", a.ClienteId);
                cmd.Parameters.AddWithValue("@auto", a.AutoId);
                cmd.Parameters.AddWithValue("@inicio", a.FechaInicio);
                cmd.Parameters.AddWithValue("@fin", a.FechaFin);
                cmd.Parameters.AddWithValue("@total", a.Total);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        */
        //

        public Alquiler ObtenerPorId(int id)
        {
            Alquiler a = null;

            using (SqlConnection con = cn.GetConnection())
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
                            AutoId = dr.GetInt32(2),
EstadoAlquiler = dr.GetString(6),
                        };
                    }
                }
            }

            return a;
        }

        /*
        public Alquiler ObtenerPorId(int id)
        {
            Alquiler a = null;

            using (SqlConnection con = cn.GetConnection())
            {
                string sql = "SELECT * FROM Alquileres WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    a = new Alquiler
                    {
                        Id = (int)dr["Id"],
                        AutoId = (int)dr["AutoId"],
                        EstadoAlquiler = dr["EstadoAlquiler"].ToString()
                    };
                }
            }

            return a;
        }
        */


        //Metodo Finalizar
      /*  public void Finalizar(int id)
        {
            using (SqlConnection con = cn.GetConnection())
            {
                string sql = "UPDATE Alquileres SET EstadoAlquiler = 'Finalizado' WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
      */

        public void Finalizar(int id)
        {
            using (SqlConnection con = cn.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_finalizar_alquiler", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        //
        /*
        public int AlquileresActivos()
        {
            using (SqlConnection con = cn.GetConnection())
            {
                string sql = "SELECT COUNT(*) FROM Alquileres WHERE Estado = 1 AND EstadoAlquiler = 'Activo'";
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();
                return (int)cmd.ExecuteScalar();
            }
        }
        */

        public int AlquileresActivos()
{
    using (SqlConnection con = cn.GetConnection())
    {
        SqlCommand cmd = new SqlCommand("sp_alquileres_activos", con);
        cmd.CommandType = CommandType.StoredProcedure;

        con.Open();
        return Convert.ToInt32(cmd.ExecuteScalar());
    }
}

        //

        public List<dynamic> ReporteAlquileres()
        {
            List<dynamic> lista = new List<dynamic>();

            using (SqlConnection con = cn.GetConnection())
            {
                string sql = @"
        SELECT 
            C.Nombres + ' ' + C.Apellidos AS Cliente,
            AU.Placa AS Placa,
            AU.Marca + ' ' + AU.Modelo AS Auto,
            A.FechaInicio,
            A.FechaFin,
            A.Total,
            A.EstadoAlquiler
        FROM Alquileres A
        INNER JOIN Clientes C ON A.ClienteId = C.Id
        INNER JOIN Autos AU ON A.AutoId = AU.Id
        WHERE A.Estado = 1
        ORDER BY A.FechaInicio DESC";

                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new
                    {
                        Cliente = dr["Cliente"].ToString(),
                        Placa = dr["Placa"].ToString(),
                        Auto = dr["Auto"].ToString(),
                        FechaInicio = dr["FechaInicio"],
                        FechaFin = dr["FechaFin"],
                        Total = dr["Total"],
                        Estado = dr["EstadoAlquiler"]
                    });
                }
            }

            return lista;
        }

        //Metodo obtener alquileres por mes

        public Dictionary<string, int> ObtenerAlquileresPorMes()
        {
            Dictionary<string, int> datos = new Dictionary<string, int>();

            using (SqlConnection con = cn.GetConnection())
            {
                string sql = @"
            SELECT 
                MONTH(FechaInicio) AS Mes,
                COUNT(*) AS Total
            FROM Alquileres
            GROUP BY MONTH(FechaInicio)
            ORDER BY Mes";

                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    int mes = Convert.ToInt32(dr["Mes"]);
                    int total = Convert.ToInt32(dr["Total"]);

                    string nombreMes = new DateTime(2024, mes, 1)
                                        .ToString("MMMM");

                    datos.Add(nombreMes, total);
                }
            }

            return datos;
        }

        //buscar alquiler

        public List<Alquiler> Buscar(string texto)
        {
            List<Alquiler> lista = new List<Alquiler>();

            using (SqlConnection con = cn.GetConnection())
            {
                string sql = @"
            SELECT 
                A.Id,
                A.FechaInicio,
                A.FechaFin,
                A.Total,
                A.Estado,
                C.Nombres + ' ' + C.Apellidos AS ClienteNombre,
                AU.Marca + ' ' + AU.Modelo AS AutoNombre
            FROM Alquileres A
            INNER JOIN Clientes C ON A.ClienteId = C.Id
            INNER JOIN Autos AU ON A.AutoId = AU.Id
            WHERE A.Estado = 1
              AND (
                    LOWER(C.Nombres) LIKE LOWER(@texto) OR
                    LOWER(C.Apellidos) LIKE LOWER(@texto) OR
                    LOWER(AU.Marca) LIKE LOWER(@texto) OR
                    LOWER(AU.Modelo) LIKE LOWER(@texto)
                  );
        ";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@texto", System.Data.SqlDbType.VarChar)
                              .Value = "%" + texto.Trim() + "%";

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Alquiler
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        FechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                        FechaFin = Convert.ToDateTime(dr["FechaFin"]),
                        Total = Convert.ToDecimal(dr["Total"]),
                        Estado = Convert.ToBoolean(dr["Estado"]),
                        ClienteNombre = dr["ClienteNombre"].ToString(),
                        AutoNombre = dr["AutoNombre"].ToString()
                    });
                }
            }

            return lista;
        }



    }
}
