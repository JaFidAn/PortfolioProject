using Application.Cqrs.Queries;
using Application.Repositories;
using Dapper;

namespace Persistence.Cqrs.Queries;

public class ProjectTechnologyQuery : IProjectTechnologyQuery
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly string _getTechnologiesByProjectSql = @"
SELECT TechnologyId FROM ProjectTechnology WHERE ProjectId = @ProjectId;";

    private readonly string _getProjectsByTechnologySql = @"
SELECT ProjectId FROM ProjectTechnology WHERE TechnologyId = @TechnologyId;";

    private readonly string _existsSql = @"
SELECT COUNT(1) FROM ProjectTechnology 
WHERE ProjectId = @ProjectId AND TechnologyId = @TechnologyId;";

    public ProjectTechnologyQuery(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Guid>> GetTechnologiesByProjectIdAsync(Guid projectId)
    {
        return await _unitOfWork.GetConnection().QueryAsync<Guid>(_getTechnologiesByProjectSql,
            new { ProjectId = projectId });
    }

    public async Task<IEnumerable<Guid>> GetProjectsByTechnologyIdAsync(Guid technologyId)
    {
        return await _unitOfWork.GetConnection().QueryAsync<Guid>(_getProjectsByTechnologySql,
            new { TechnologyId = technologyId });
    }

    public async Task<bool> ExistsAsync(Guid projectId, Guid technologyId)
    {
        var count = await _unitOfWork.GetConnection().ExecuteScalarAsync<int>(_existsSql,
            new { ProjectId = projectId, TechnologyId = technologyId });

        return count > 0;
    }
}
