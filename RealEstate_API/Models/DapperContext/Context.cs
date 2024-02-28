using Microsoft.Data.SqlClient;
using System.Data;

namespace RealEstate_API.Models.DapperContext
{
    public class Context(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        private readonly string _connectionString = configuration.GetConnectionString("DefaulConnection") ?? throw new ArgumentNullException("Cannot Connect");

        public IDbConnection GetConnection() => new SqlConnection(_connectionString);
    }
}
