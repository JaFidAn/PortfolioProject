using System.Data;
using Application.Repositories;
using Microsoft.Extensions.Configuration;
using Dapper;

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
        AddTypeHandlers();
    }

    public IDbConnection GetMasterConnection()
    {
        return _connectionFactory.CreateConnection(_connectionString);
    }

    public IDbTransaction BeginTransaction()
    {
        EnsureConnectionOpen();
        _transaction = _connection!.BeginTransaction();
        Console.WriteLine("[UnitOfWork] Transaction started.");
        return _transaction;
    }

    public IDbConnection GetConnection()
    {
        EnsureConnectionOpen();
        Console.WriteLine($"[UnitOfWork] Returning database connection. State: {_connection!.State}");
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

    private void AddTypeHandlers()
    {
        SqlMapper.AddTypeHandler(new DateTimeHandler());
        SqlMapper.AddTypeHandler(new SqlTimeOnlyTypeHandler());
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _transaction?.Dispose();
                CloseConnection();
                _connection?.Dispose(); // Connection-u tam bağlayırıq
            }

            _disposed = true;
        }
    }

    private void EnsureConnectionOpen()
    {
        if (_connection == null || _connection.State == ConnectionState.Closed)
        {
            Console.WriteLine("[UnitOfWork] Creating new database connection...");
            _connection = _connectionFactory.CreateConnection(_connectionString);
        }

        if (_connection.State != ConnectionState.Open)
        {
            Console.WriteLine("[UnitOfWork] Opening database connection...");
            _connection.Open();
        }
    }

    private void CloseConnection()
    {
        if (_connection?.State == ConnectionState.Open)
        {
            Console.WriteLine("[UnitOfWork] Closing database connection...");
            _connection.Close();
            _connection = null; // Yenidən connection yaratmaq üçün `null` edirik
        }
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }
}

public class DateTimeHandler : SqlMapper.TypeHandler<DateTime>
{
    public override void SetValue(IDbDataParameter parameter, DateTime value)
    {
        parameter.Value = TimeZoneInfo.ConvertTimeToUtc(value);
    }

    public override DateTime Parse(object value)
    {
        var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
        var utcDateTime = DateTime.SpecifyKind((DateTime)value, DateTimeKind.Utc);
        return TimeZoneInfo.ConvertTime(utcDateTime, timeZoneInfo);
    }
}

public class SqlTimeOnlyTypeHandler : SqlMapper.TypeHandler<TimeOnly>
{
    public override void SetValue(IDbDataParameter parameter, TimeOnly time)
    {
        parameter.Value = time.ToString();
    }

    public override TimeOnly Parse(object value)
    {
        return TimeOnly.FromTimeSpan((TimeSpan)value);
    }
}
