using System.Data;

namespace Application.Repositories;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection(string connectionString);
}
