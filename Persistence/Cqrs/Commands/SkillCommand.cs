using Application.Cqrs.Commands;
using Application.Repositories;
using Dapper;
using Domain.Entities;

namespace Persistence.Cqrs.Commands;

public class SkillCommand : ISkillCommand
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly string _createSql = @"
INSERT INTO Skills (Id, Name, Level, CreatedAt, UpdatedAt, IsDeleted)
OUTPUT INSERTED.Id
VALUES (
    @Id,
    @Name,
    @Level,
    GETUTCDATE(),
    GETUTCDATE(),
    0
);";

    private readonly string _updateSql = @"
UPDATE Skills 
SET 
    Name = @Name,
    Level = @Level,
    UpdatedAt = GETUTCDATE()
WHERE Id = @Id AND IsDeleted = 0;";

    private readonly string _deleteSql = "UPDATE Skills SET IsDeleted = 1 WHERE Id = @Id;";

    public SkillCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> CreateAsync(Skill model)
    {
        var parameters = new
        {
            Id = model.Id,
            Name = model.Name,
            Level = model.Level
        };

        return await _unitOfWork.GetConnection()
            .QuerySingleAsync<Guid>(_createSql, parameters);
    }

    public async Task UpdateAsync(Skill model)
    {
        var parameters = new
        {
            Id = model.Id,
            Name = model.Name,
            Level = model.Level
        };

        await _unitOfWork.GetConnection()
            .ExecuteAsync(_updateSql, parameters);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _unitOfWork.GetConnection().ExecuteAsync(_deleteSql, new { Id = id });
    }
}
