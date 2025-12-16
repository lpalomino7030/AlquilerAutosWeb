using System.Collections.Generic;
using Microsoft.Data.SqlClient;


namespace AlquilerAutosWeb.Models
{
    public class AutoDAO
    {
        Conexion cn = new Conexion();

        public List<Auto> Listar()
        {
            List<Auto> lista = new List<Auto>();

            using (SqlConnection con = cn.GetConnection())
            {
                string sql = "SELECT * FROM Autos WHERE Estado = 1";
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Auto
                    {
                        Id = (int)dr["Id"],
                        Placa = dr["Placa"].ToString(),
                        Marca = dr["Marca"].ToString(),
                        Modelo = dr["Modelo"].ToString(),
                        Anio = (int)dr["Anio"],
                        PrecioDia = (decimal)dr["PrecioDia"],
                        EstadoAuto = dr["EstadoAuto"].ToString(),
                        Estado = (bool)dr["Estado"]
                    });
                }
            }

            return lista;
        }

        //


        public void Insertar(Auto a)
        {
            using (SqlConnection con = cn.GetConnection())
            {
                string sql = @"INSERT INTO Autos
                       (Placa, Marca, Modelo, Anio, PrecioDia, EstadoAuto, Estado)
                       VALUES
                       (@placa, @marca, @modelo, @anio, @precio, 'Disponible', 1)";

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@placa", a.Placa);
                cmd.Parameters.AddWithValue("@marca", a.Marca);
                cmd.Parameters.AddWithValue("@modelo", a.Modelo);
                cmd.Parameters.AddWithValue("@anio", a.Anio);
                cmd.Parameters.AddWithValue("@precio", a.PrecioDia);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //

        public Auto ObtenerPorId(int id)
        {
            Auto a = null;

            using (SqlConnection con = cn.GetConnection())
            {
                string sql = "SELECT * FROM Autos WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    a = new Auto
                    {
                        Id = (int)dr["Id"],
                        Placa = dr["Placa"].ToString(),
                        Marca = dr["Marca"].ToString(),
                        Modelo = dr["Modelo"].ToString(),
                        Anio = (int)dr["Anio"],
                        PrecioDia = (decimal)dr["PrecioDia"],
                        EstadoAuto = dr["EstadoAuto"].ToString(),
                        Estado = (bool)dr["Estado"]
                    };
                }
            }

            return a;
        }

        //
        public void Actualizar(Auto a)
        {
            using (SqlConnection con = cn.GetConnection())
            {
                string sql = @"UPDATE Autos SET
                       Placa = @placa,
                       Marca = @marca,
                       Modelo = @modelo,
                       Anio = @anio,
                       PrecioDia = @precio
                       WHERE Id = @id";

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@placa", a.Placa);
                cmd.Parameters.AddWithValue("@marca", a.Marca);
                cmd.Parameters.AddWithValue("@modelo", a.Modelo);
                cmd.Parameters.AddWithValue("@anio", a.Anio);
                cmd.Parameters.AddWithValue("@precio", a.PrecioDia);
                cmd.Parameters.AddWithValue("@id", a.Id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //
        public void Eliminar(int id)
        {
            using (SqlConnection con = cn.GetConnection())
            {
                string sql = "UPDATE Autos SET Estado = 0 WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        //


        public List<Auto> ListarDisponibles()
        {
            List<Auto> lista = new List<Auto>();

            using (SqlConnection con = cn.GetConnection())
            {
                string sql = "SELECT * FROM Autos WHERE Estado = 1 AND EstadoAuto = 'Disponible'";
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Auto
                    {
                        Id = (int)dr["Id"],
                        Placa = dr["Placa"].ToString(),
                        Marca = dr["Marca"].ToString(),
                        Modelo = dr["Modelo"].ToString(),
                        PrecioDia = (decimal)dr["PrecioDia"]
                
                    });

                }
            }

            return lista;
        }

        //  

        public Auto ObtenerPrecio(int id)
        {
            Auto a = null;

            using (SqlConnection con = cn.GetConnection())
            {
                string sql = "SELECT * FROM Autos WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    a = new Auto
                    {
                        Id = (int)dr["Id"],
                        PrecioDia = (decimal)dr["PrecioDia"]
                    };
                }
            }

            return a;
        }

        //
        public void MarcarAlquilado(int id)
        {
            using (SqlConnection con = cn.GetConnection())
            {
                string sql = "UPDATE Autos SET EstadoAuto = 'Alquilado' WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //

        public void MarcarDisponible(int id)
        {
            using (SqlConnection con = cn.GetConnection())
            {
                string sql = "UPDATE Autos SET EstadoAuto = 'Disponible' WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        //


        public int TotalAutos()
        {
            using (SqlConnection con = cn.GetConnection())
            {
                string sql = "SELECT COUNT(*) FROM Autos WHERE Estado = 1";
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public int AutosDisponibles()
        {
            using (SqlConnection con = cn.GetConnection())
            {
                string sql = "SELECT COUNT(*) FROM Autos WHERE Estado = 1 AND EstadoAuto = 'Disponible'";
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        //metodo que compara autos disponibles con los queno 

        public Dictionary<string, int> ObtenerEstadoAutos()
        {
            Dictionary<string, int> datos = new Dictionary<string, int>();

            using (SqlConnection con = cn.GetConnection())
            {
                string sql = @"
            SELECT EstadoAuto, COUNT(*) AS Total
            FROM Autos
            WHERE Estado = 1
            GROUP BY EstadoAuto";

                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    datos.Add(
                        dr["EstadoAuto"].ToString(),
                        Convert.ToInt32(dr["Total"])
                    );
                }
            }

            return datos;
        }

        //buscar auto
        public List<Auto> Buscar(string texto)
        {
            List<Auto> lista = new List<Auto>();

            using (SqlConnection con = cn.GetConnection())
            {
                string sql = @"SELECT * FROM Autos 
                       WHERE Estado = 1
                       AND (
                            Placa LIKE @texto OR
                            Marca LIKE @texto OR
                            Modelo LIKE @texto
                       )";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@texto", "%" + texto + "%");

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Auto
                    {
                        Id = (int)dr["Id"],
                        Placa = dr["Placa"].ToString(),
                        Marca = dr["Marca"].ToString(),
                        Modelo = dr["Modelo"].ToString(),
                        Anio = (int)dr["Anio"],
                        PrecioDia = (decimal)dr["PrecioDia"],
                        EstadoAuto = dr["EstadoAuto"].ToString(),
                        Estado = (bool)dr["Estado"]
                    });
                }
            }

            return lista;
        }

    }
}
