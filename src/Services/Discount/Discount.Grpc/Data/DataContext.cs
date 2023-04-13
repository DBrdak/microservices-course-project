using Npgsql;

namespace Discount.Grpc.Data;

public class DataContext
{
    private readonly IConfiguration _configuration;

    public DataContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public NpgsqlConnection CreateConnection()
    {
        var connectionString = _configuration.GetValue<string>("DatabaseSettings:ConnectionString");
        return new NpgsqlConnection(connectionString);
    }
}