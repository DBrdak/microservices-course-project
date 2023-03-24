using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;

namespace Discount.API.Data
{
    public class DataContext
    {
        private readonly DbSettings _dbSettings;

        public DataContext(IOptions<DbSettings> dbSettings)
        {
            _dbSettings = dbSettings.Value;
        }

        public IDbConnection CreateConnection()
        {
            var connectionString = $"Host={_dbSettings.Server}; Port={_dbSettings.Port}; Database={_dbSettings.Database}; Username={_dbSettings.UserId}; Password={_dbSettings.Password};";
            return new NpgsqlConnection(connectionString);
        }
    }
}

