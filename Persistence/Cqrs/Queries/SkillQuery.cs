using Application.Cqrs.Queries;
using Application.Repositories;
using Dapper;
using Domain.Entities;

namespace Persistence.Cqrs.Queries;

public class SkillQuery : ISkillQuery
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly string _getAllSql = @"
        SELECT * FROM Skills 
        WHERE IsDeleted = 0 
        ORDER BY CreatedAt DESC";

    private readonly string _getByIdSql = @"
        SELECT * FROM Skills 
        WHERE Id = @Id AND IsDeleted = 0";

    public SkillQuery(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Skill>> GetAllAsync()
    {
        return await _unitOfWork.GetConnection().QueryAsync<Skill>(_getAllSql);
    }

    public async Task<Skill?> GetByIdAsync(Guid id)
    {
        return await _unitOfWork.GetConnection().QuerySingleOrDefaultAsync<Skill>(_getByIdSql, new { Id = id });
    }
}
