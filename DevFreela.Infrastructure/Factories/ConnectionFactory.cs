using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Factories
{
    public class DatabaseConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public DatabaseConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            DevFreelaConnectionString = _configuration.GetConnectionString("DevFreelaConnectionString");
        }

        public string DevFreelaConnectionString { get; private set; }

        public SqlConnection GetConnectionDevFreela() => new SqlConnection(DevFreelaConnectionString);

    }
}
