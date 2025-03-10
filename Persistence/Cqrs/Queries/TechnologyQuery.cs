using Application.Cqrs.Queries;
using Application.Repositories;
using Dapper;
using Domain.Entities;

namespace Persistence.Cqrs.Queries;

public class TechnologyQuery : ITechnologyQuery
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly string _getAllSql = @"
        SELECT * FROM Technologies 
        WHERE IsDeleted = 0 
        ORDER BY CreatedAt DESC";

    private readonly string _getByIdSql = @"
        SELECT * FROM Technologies 
        WHERE Id = @Id AND IsDeleted = 0";

    public TechnologyQuery(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Technology>> GetAllAsync()
    {
        using var connection = _unitOfWork.GetConnection();
        return await connection.QueryAsync<Technology>(_getAllSql);
    }

    public async Task<Technology?> GetByIdAsync(Guid id)
    {
        using var connection = _unitOfWork.GetConnection();
        return await connection.QuerySingleOrDefaultAsync<Technology>(_getByIdSql, new { Id = id });
    }
}
