using Application.Cqrs.Queries;
using Application.Repositories;
using Dapper;
using Domain.Entities;

namespace Persistence.Cqrs.Queries;

public class ProjectQuery : IProjectQuery
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly string _getAllSql = @"
        SELECT * FROM Projects 
        WHERE IsDeleted = 0 
        ORDER BY CreatedAt DESC";

    private readonly string _getByIdSql = @"
        SELECT * FROM Projects 
        WHERE Id = @Id AND IsDeleted = 0";

    public ProjectQuery(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        using var connection = _unitOfWork.GetConnection();
        return await connection.QueryAsync<Project>(_getAllSql);
    }

    public async Task<Project?> GetByIdAsync(Guid id)
    {
        using var connection = _unitOfWork.GetConnection();
        return await connection.QuerySingleOrDefaultAsync<Project>(_getByIdSql, new { Id = id });
    }
}
