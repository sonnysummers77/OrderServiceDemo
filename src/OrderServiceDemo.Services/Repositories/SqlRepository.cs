using Microsoft.Extensions.Options;
using OrderServiceDemo.Models.Options;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace OrderServiceDemo.Services.Repositories
{
    public abstract class SqlRepository
    {
        private readonly string _dbConnectionString;

        public SqlRepository(IOptionsSnapshot<OrderServiceDemoOptions> options)
        {
            _dbConnectionString = options.Value.OrderDBConnection;
        }

        public async Task<SqlConnection> GetConnection()
        {
            var conn = new SqlConnection(_dbConnectionString);
            await conn.OpenAsync();
            return conn;
        }
    }
}
