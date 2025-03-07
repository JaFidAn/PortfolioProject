using System.Data;

namespace Application.Repositories;

public interface IUnitOfWork : IDisposable
{
    IDbTransaction BeginTransaction();
    IDbConnection GetConnection();
    IDbTransaction GetTransaction();
    IDbConnection GetMasterConnection();
    void SaveChanges();
    void Rollback();
    Task SaveChangesAsync();
}
