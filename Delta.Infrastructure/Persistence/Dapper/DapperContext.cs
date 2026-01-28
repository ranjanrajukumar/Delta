using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Delta.Infrastructure.Persistence.Dapper
{
    public class DapperContext : IDapperContext
    {
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<int> EditDataAsync(string command, object parameters)
        {
            using var db = CreateConnection();
            return await db.ExecuteAsync(command, parameters);
        }

        public async Task<List<T>> GetAllAsync<T>(string command, object parameters)
        {
            using var db = CreateConnection();
            var result = await db.QueryAsync<T>(command, parameters);
            return result.ToList();
        }

        public async Task<T?> GetAsync<T>(string command, object parameters)
        {
            using var db = CreateConnection();
            return await db.QueryFirstOrDefaultAsync<T>(command, parameters);
        }
    }
}
