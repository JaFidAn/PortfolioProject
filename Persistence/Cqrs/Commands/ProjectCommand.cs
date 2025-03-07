using Application.Cqrs.Commands;
using Application.Repositories;
using Dapper;
using Domain.Entities;

namespace Persistence.Cqrs.Commands;

public class ProjectCommand : IProjectCommand
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly string _createSql = $@"
INSERT INTO Projects (Id, Title, Description, Link, CreatedAt, UpdatedAt, IsDeleted)
OUTPUT INSERTED.Id
VALUES (
    @{nameof(Project.Id)},
    @{nameof(Project.Title)},
    @{nameof(Project.Description)},
    @{nameof(Project.Link)},
    GETUTCDATE(),
    GETUTCDATE(),
    0
);";

    private readonly string _updateSql = $@"
UPDATE Projects 
SET 
    {nameof(Project.Title)} = @{nameof(Project.Title)},
    {nameof(Project.Description)} = @{nameof(Project.Description)},
    {nameof(Project.Link)} = @{nameof(Project.Link)},
    UpdatedAt = GETUTCDATE()
WHERE Id = @Id AND IsDeleted = 0;";

    private readonly string _deleteSql = "UPDATE Projects SET IsDeleted = 1 WHERE Id = @Id;";

    public ProjectCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> CreateAsync(Project model)
    {
        var result = await _unitOfWork.GetConnection()
            .QuerySingleAsync<Guid>(_createSql, model, _unitOfWork.GetTransaction()).ConfigureAwait(false);
        return result;
    }

    public async Task UpdateAsync(Project model)
    {
        await _unitOfWork.GetConnection().ExecuteAsync(_updateSql, model, _unitOfWork.GetTransaction()).ConfigureAwait(false);
    }

    public async Task DeleteAsync(Guid id)
    {
        var parameter = new
        {
            id
        };
        await _unitOfWork.GetConnection().ExecuteAsync(_deleteSql, parameter, _unitOfWork.GetTransaction()).ConfigureAwait(false);
    }

}
