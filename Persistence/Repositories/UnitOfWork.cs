using System.Data;
using Application.Repositories;
using Microsoft.Extensions.Configuration;
using Persistence.TypeHandlers;

namespace Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly string _connectionString;
    private IDbConnection? _connection;
    private bool _disposed;
    private IDbTransaction? _transaction;

    public UnitOfWork(IConfiguration configuration, IDbConnectionFactory connectionFactory)
    {
        _connectionString = configuration.GetConnectionString("SqlServerConnection")
            ?? throw new InvalidOperationException("Connection string 'SqlServerConnection' is missing in appsettings.json.");

        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        DapperTypeHandlerRegister.RegisterHandlers();
    }

    public IDbConnection GetMasterConnection()
    {
        return _connectionFactory.CreateConnection(_connectionString);
    }

    public IDbTransaction BeginTransaction()
    {
        EnsureConnectionOpen();
        _transaction = _connection!.BeginTransaction();
        return _transaction;
    }

    public IDbConnection GetConnection()
    {
        EnsureConnectionOpen();
        return _connection!;
    }

    public IDbTransaction? GetTransaction()
    {
        return _transaction;
    }

    public void SaveChanges()
    {
        if (_transaction?.Connection != null)
        {
            _transaction.Commit();
            CloseConnection();
            _transaction = null;
        }
    }

    public async Task SaveChangesAsync()
    {
        if (_transaction?.Connection != null)
        {
            await Task.Run(() => _transaction.Commit());
            CloseConnection();
            _transaction = null;
        }
    }

    public void Rollback()
    {
        if (_transaction?.Connection != null)
        {
            _transaction.Rollback();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _transaction?.Dispose();
                CloseConnection();
                _connection?.Dispose();
            }

            _disposed = true;
        }
    }

    private void EnsureConnectionOpen()
    {
        if (_connection == null || _connection.State == ConnectionState.Closed)
        {
            _connection = _connectionFactory.CreateConnection(_connectionString);
        }

        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }
    }

    private void CloseConnection()
    {
        if (_connection?.State == ConnectionState.Open)
        {
            _connection.Close();
            _connection = null;
        }
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }
}