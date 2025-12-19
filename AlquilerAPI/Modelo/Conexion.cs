using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace AlquilerAPI.Models
{
    public class Conexion
    {
        private readonly string _cadena;

        public Conexion()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _cadena = config.GetConnectionString("cn");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_cadena);
        }
    }
}
