using Microsoft.Data.SqlClient;
using BCrypt.Net;

namespace AlquilerAutosWeb.Models
{
    public class UsuarioDAO
    {
        Conexion cn = new Conexion();

        public void CrearUsuarioAdmin()
        {
            using (SqlConnection con = cn.GetConnection())
            {
                string sqlCheck = "SELECT COUNT(*) FROM Usuarios WHERE Usuario = 'admin'";
                SqlCommand checkCmd = new SqlCommand(sqlCheck, con);

                con.Open();
                int existe = (int)checkCmd.ExecuteScalar();

                if (existe == 0)
                {
                    string sqlInsert = @"INSERT INTO Usuarios (Usuario, Password, Rol, Estado) VALUES (@usuario, @password, @rol, @estado)";

                    SqlCommand insertCmd = new SqlCommand(sqlInsert, con);

                    string passwordHash = BCrypt.Net.BCrypt.HashPassword("123");
                    Console.WriteLine(passwordHash);
                    insertCmd.Parameters.AddWithValue("@usuario", "admin");
                    insertCmd.Parameters.AddWithValue("@password", passwordHash);
                    insertCmd.Parameters.AddWithValue("@rol", "Administrador");
                    insertCmd.Parameters.AddWithValue("@estado", 1);

                    insertCmd.ExecuteNonQuery();
                }
            }
        }



        public Usuario ValidarLogin(string usuario, string password)
        {
            Usuario u = null;

            using (SqlConnection con = cn.GetConnection())
            {
                string sql = "SELECT * FROM Usuarios WHERE Usuario = @usuario AND Estado = 1";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@usuario", usuario);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    string passwordHash = dr.GetString(0);

                    if (BCrypt.Net.BCrypt.Verify(password, passwordHash))
                    {
                        u = new Usuario
                        {
                            Id = dr.GetInt32(0),
                            UsuarioNombre = dr.GetString(1),
                            Rol = dr.GetString(2),
                            Estado =dr.GetBoolean(3)
                        };
                    }
                }
            }

            return u;
        }


    }
}
