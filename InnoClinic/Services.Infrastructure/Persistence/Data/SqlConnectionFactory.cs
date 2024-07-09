using Microsoft.Extensions.Configuration;
public class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly IConfiguration _configuration;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public SqlConnection CreateConnection()
    {
        return new SqlConnection(
            _configuration.GetConnectionString("ServicesDatabase"));
    }
}
