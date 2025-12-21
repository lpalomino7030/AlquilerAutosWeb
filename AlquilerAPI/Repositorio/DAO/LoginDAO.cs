using AlquilerAPI.Models;
using AlquilerAPI.Repositorio.Intefaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AlquilerAPI.Repositorio.DAO
{
    public class LoginDAO : ILogin
    {
        private readonly string cadena;

        public LoginDAO()
        {
            cadena = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("cn");
        }

        public Usuarios? ValidarLogin(string usuario, string clave)
        {
            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_login_usuario", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Usuario", usuario);
                cmd.Parameters.AddWithValue("@Password", clave);

                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    return new Usuarios
                    {
                        Id = Convert.ToInt32(rd["Id"]),
                        Usuario = rd["Usuario"].ToString(),
                        Rol = rd["Rol"].ToString(),
                        Estado = Convert.ToBoolean(rd["Estado"])
                    };
                }
            }

            return null;
        }
    }
}

