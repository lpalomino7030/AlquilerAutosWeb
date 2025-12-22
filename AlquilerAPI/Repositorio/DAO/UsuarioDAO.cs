using AlquilerAPI.Models;
using AlquilerAPI.Repositorio.Intefaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AlquilerAPI.Repositorio.DAO
{
    public class UsuarioDAO : IUsuario
    {

        private readonly string cadena = string.Empty;


        public UsuarioDAO()
        {
            cadena = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build()
            .GetConnectionString("cn");
        }

        public int InsertarUsuario(Usuarios usuario)
        {
            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarUsuario", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UsuarioNombre", usuario.Usuario);
                cmd.Parameters.AddWithValue("@Password", usuario.Password);
                cmd.Parameters.AddWithValue("@Rol", usuario.Rol);
                cmd.Parameters.AddWithValue("@Estado", usuario.Estado);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public int UpdateUsuario(Usuarios usuario)
        {
            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateUsuario", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", usuario.Id);
                cmd.Parameters.AddWithValue("@Usuario", usuario.Usuario);
                cmd.Parameters.AddWithValue("@Password", usuario.Password);
                cmd.Parameters.AddWithValue("@Rol", usuario.Rol);
                cmd.Parameters.AddWithValue("@Estado", usuario.Estado);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public List<Usuarios> ListarUsuario()
        {
            List<Usuarios> lista = new List<Usuarios>();

            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_listar_usuarios", con);
                cmd.CommandType = CommandType.StoredProcedure;


                con.Open();

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {

                    lista.Add(new Usuarios
                    {
                        Id = Convert.ToInt32(rd["Id"]),
                        Usuario = rd["Usuario"].ToString(),
                        Password = string.Empty,
                        Rol = rd["Rol"].ToString(),
                        Estado = Convert.ToBoolean(rd["Estado"])
                    });
                }

                }            return lista;
        }

    }
}
