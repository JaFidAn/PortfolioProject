using Application.Cqrs.Commands;
using Application.Repositories;
using Dapper;
using Domain.Entities;

namespace Persistence.Cqrs.Commands;

public class TechnologyCommand : ITechnologyCommand
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly string _createSql = $@"
INSERT INTO Technologies (Id, Name, CreatedAt, UpdatedAt, IsDeleted)
OUTPUT INSERTED.Id
VALUES (
    @{nameof(Technology.Id)},
    @{nameof(Technology.Name)},
    GETUTCDATE(),
    GETUTCDATE(),
    0
);";

    private readonly string _updateSql = $@"
UPDATE Technologies 
SET 
    {nameof(Technology.Name)} = @{nameof(Technology.Name)},
    UpdatedAt = GETUTCDATE()
WHERE Id = @Id AND IsDeleted = 0;";

    private readonly string _deleteSql = "UPDATE Technologies SET IsDeleted = 1 WHERE Id = @Id;";

    public TechnologyCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> CreateAsync(Technology model)
    {
        var result = await _unitOfWork.GetConnection()
            .QuerySingleAsync<Guid>(_createSql, model, _unitOfWork.GetTransaction()).ConfigureAwait(false);
        return result;
    }

    public async Task UpdateAsync(Technology model)
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
