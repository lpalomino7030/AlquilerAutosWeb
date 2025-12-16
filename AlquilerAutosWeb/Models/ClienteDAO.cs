using Microsoft.Data.SqlClient;
using System.Collections.Generic;


namespace AlquilerAutosWeb.Models
{
    public class ClienteDAO
    {
        Conexion cn = new Conexion();

        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection con = cn.GetConnection())
            {
                string sql = "SELECT * FROM Clientes WHERE Estado = 1";
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Cliente c = new Cliente
                    {
                        Id = (int)dr["Id"],
                        DNI = dr["DNI"].ToString(),
                        Nombres = dr["Nombres"].ToString(),
                        Apellidos = dr["Apellidos"].ToString(),
                        Telefono = dr["Telefono"].ToString(),
                        Email = dr["Email"].ToString(),
                        Estado = (bool)dr["Estado"]
                    };

                    lista.Add(c);
                }
            }

            return lista;
        }

        //

        public void Insertar(Cliente c)
        {
            using (SqlConnection con = cn.GetConnection())
            {
                string sql = @"INSERT INTO Clientes
                       (DNI, Nombres, Apellidos, Telefono, Email, Estado)
                       VALUES
                       (@dni, @nombres, @apellidos, @telefono, @email, 1)";

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@dni", c.DNI);
                cmd.Parameters.AddWithValue("@nombres", c.Nombres);
                cmd.Parameters.AddWithValue("@apellidos", c.Apellidos);
                cmd.Parameters.AddWithValue("@telefono", c.Telefono);
                cmd.Parameters.AddWithValue("@email", c.Email);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        //

        public Cliente ObtenerPorId(int id)
        {
            Cliente c = null;

            using (SqlConnection con = cn.GetConnection())
            {
                string sql = "SELECT * FROM Clientes WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    c = new Cliente
                    {
                        Id = (int)dr["Id"],
                        DNI = dr["DNI"].ToString(),
                        Nombres = dr["Nombres"].ToString(),
                        Apellidos = dr["Apellidos"].ToString(),
                        Telefono = dr["Telefono"].ToString(),
                        Email = dr["Email"].ToString(),
                        Estado = (bool)dr["Estado"]
                    };
                }
            }

            return c;
        }

        public void Actualizar(Cliente c)
        {
            using (SqlConnection con = cn.GetConnection())
            {
                string sql = @"UPDATE Clientes SET
                       DNI = @dni,
                       Nombres = @nombres,
                       Apellidos = @apellidos,
                       Telefono = @telefono,
                       Email = @email
                       WHERE Id = @id";

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@dni", c.DNI);
                cmd.Parameters.AddWithValue("@nombres", c.Nombres);
                cmd.Parameters.AddWithValue("@apellidos", c.Apellidos);
                cmd.Parameters.AddWithValue("@telefono", c.Telefono);
                cmd.Parameters.AddWithValue("@email", c.Email);
                cmd.Parameters.AddWithValue("@id", c.Id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //
        public void Eliminar(int id)
        {
            using (SqlConnection con = cn.GetConnection())
            {
                string sql = "UPDATE Clientes SET Estado = 0 WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // 

        public List<Cliente> ListarActivos()
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection con = cn.GetConnection())
            {
                string sql = "SELECT * FROM Clientes WHERE Estado = 1";
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Cliente
                    {
                        Id = (int)dr["Id"],
                        Nombres = dr["Nombres"].ToString(),
                        Apellidos = dr["Apellidos"].ToString()
                    });
                }
            }

            return lista;
        }

        //

        public int TotalClientes()
        {
            using (SqlConnection con = cn.GetConnection())
            {
                string sql = "SELECT COUNT(*) FROM Clientes WHERE Estado = 1";
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        //metodo buscar 

        public List<Cliente> Buscar(string texto)
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection con = cn.GetConnection())
            {
                string sql = @"SELECT * FROM Clientes
                       WHERE Estado = 1
                       AND (
                            Nombres LIKE @texto OR
                            Apellidos LIKE @texto OR
                            DNI LIKE @texto
                       )";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@texto", "%" + texto + "%");

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Cliente
                    {
                        Id = (int)dr["Id"],
                        Nombres = dr["Nombres"].ToString(),
                        Apellidos = dr["Apellidos"].ToString(),
                        DNI = dr["DNI"].ToString(),
                        Estado = (bool)dr["Estado"]
                    });
                }
            }

            return lista;
        }


    }
}
