using System.Data;
using Application.Repositories;
using Microsoft.Data.SqlClient;

namespace Persistence.Repositories;

public class SqlConnectionFactory : IDbConnectionFactory
{
    public IDbConnection CreateConnection(string connectionString)
    {
        return new SqlConnection(connectionString);
    }
}
