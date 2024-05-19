using Microsoft.Data.SqlClient;

namespace Dapper.WebAPI.Services;

public sealed class DatabaseService(IConfiguration configuration)
{
    public SqlConnection GetConnection()
    {
        return new SqlConnection(configuration.GetConnectionString("SqlServer"));
    }
}
