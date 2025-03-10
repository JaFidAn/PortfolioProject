using Application.Cqrs.Commands;
using Application.Repositories;
using Dapper;

namespace Persistence.Cqrs.Commands;

public class ProjectTechnologyCommand : IProjectTechnologyCommand
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly string _insertSql = @"
INSERT INTO ProjectTechnology (ProjectId, TechnologyId)
VALUES (@ProjectId, @TechnologyId);";

    private readonly string _deleteSql = @"
DELETE FROM ProjectTechnology 
WHERE ProjectId = @ProjectId AND TechnologyId = @TechnologyId;";

    private readonly string _deleteAllByProjectSql = @"
DELETE FROM ProjectTechnology WHERE ProjectId = @ProjectId;";

    public ProjectTechnologyCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddAsync(Guid projectId, Guid technologyId)
    {
        await _unitOfWork.GetConnection().ExecuteAsync(_insertSql,
            new { ProjectId = projectId, TechnologyId = technologyId },
            _unitOfWork.GetTransaction());
    }

    public async Task RemoveAsync(Guid projectId, Guid technologyId)
    {
        await _unitOfWork.GetConnection().ExecuteAsync(_deleteSql,
            new { ProjectId = projectId, TechnologyId = technologyId },
            _unitOfWork.GetTransaction());
    }

    public async Task RemoveAllByProjectIdAsync(Guid projectId)
    {
        await _unitOfWork.GetConnection().ExecuteAsync(_deleteAllByProjectSql,
            new { ProjectId = projectId },
            _unitOfWork.GetTransaction());
    }
}
