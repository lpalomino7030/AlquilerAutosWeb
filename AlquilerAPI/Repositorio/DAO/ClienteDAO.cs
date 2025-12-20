using AlquilerAPI.Models;
using AlquilerAPI.Repositorio.Intefaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AlquilerAPI.Repositorio.DAO
{
    public class ClienteDAO : ICliente
    {
        private readonly string cadena = string.Empty;

        public ClienteDAO()
        {

            cadena = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build()
            .GetConnectionString("cn");
        }

        public List<Cliente> BuscarCliente(string texto)
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_buscar_clientes", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Texto", SqlDbType.VarChar, 100).Value = texto;

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Cliente
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("Id")),
                        DNI = dr.GetString(dr.GetOrdinal("DNI")),
                        Nombres = dr.GetString(dr.GetOrdinal("Nombres")),
                        Apellidos = dr.GetString(dr.GetOrdinal("Apellidos")),
                        Estado = dr.GetBoolean(dr.GetOrdinal("Estado"))
                    });
                }
            }

            return lista;
        }

        public List<Cliente> ClientesActivos()
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_listar_clientes_activos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Cliente
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("Id")),
                        Nombres = dr.GetString(dr.GetOrdinal("Nombres")),
                        Apellidos = dr.GetString(dr.GetOrdinal("Apellidos"))
                    });
                }
            }

            return lista;
        }

        public int DeleteCliente(int id)
        {
            var rpta = 0;
            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_eliminar_cliente", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                return rpta = (int)cmd.ExecuteNonQuery();
            }
        }

        public int InsertarCliente(Cliente cliente)
        {
            var rpta = 0;
            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_insertar_cliente", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DNI", cliente.DNI);
                cmd.Parameters.AddWithValue("@Nombres", cliente.Nombres);
                cmd.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);

                con.Open();
                return rpta = (int)cmd.ExecuteNonQuery();
            }
        }

        public List<Cliente> ListarCliente()
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_listar_clientes", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Cliente c = new Cliente
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("Id")),
                        DNI = dr.GetString(dr.GetOrdinal("DNI")),
                        Nombres = dr.GetString(dr.GetOrdinal("Nombres")),
                        Apellidos = dr.GetString(dr.GetOrdinal("Apellidos")),
                        Telefono = dr.GetString(dr.GetOrdinal("Telefono")),
                        Email = dr.GetString(dr.GetOrdinal("Email")),
                        Estado = dr.GetBoolean(dr.GetOrdinal("Estado"))
                    };

                    lista.Add(c);
                }
            }

            return lista;
        }

        public Cliente ObtenerClienteId(int id)
        {
            Cliente c = null;

            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_obtener_cliente_por_id", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    c = new Cliente
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("Id")),
                        DNI = dr.GetString(dr.GetOrdinal("DNI")),
                        Nombres = dr.GetString(dr.GetOrdinal("Nombres")),
                        Apellidos = dr.GetString(dr.GetOrdinal("Apellidos")),
                        Telefono = dr.GetString(dr.GetOrdinal("Telefono")),
                        Email = dr.GetString(dr.GetOrdinal("Email")),
                        Estado = dr.GetBoolean(dr.GetOrdinal("Estado"))
                    };
                }
            }

            return c;
        }

        public int TotalClientes()
        {
            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_total_clientes_activos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateCliente(Cliente cliente)
        {
            var rpta = 0;
            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_actualizar_cliente", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", cliente.Id);
                cmd.Parameters.AddWithValue("@DNI", cliente.DNI);
                cmd.Parameters.AddWithValue("@Nombres", cliente.Nombres);
                cmd.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);

                con.Open();
                return rpta= (int)cmd.ExecuteNonQuery();
            }
        }









    }
}
