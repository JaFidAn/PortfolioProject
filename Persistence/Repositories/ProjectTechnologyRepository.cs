using Application.Repositories;
using Application.Cqrs.Commands;
using Application.Cqrs.Queries;

namespace Persistence.Repositories;

public class ProjectTechnologyRepository : IProjectTechnologyRepository
{
    private readonly IProjectTechnologyCommand _command;
    private readonly IProjectTechnologyQuery _query;

    public ProjectTechnologyRepository(IProjectTechnologyCommand command, IProjectTechnologyQuery query)
    {
        _command = command;
        _query = query;
    }

    public async Task AddAsync(Guid projectId, Guid technologyId)
        => await _command.AddAsync(projectId, technologyId);

    public async Task RemoveAsync(Guid projectId, Guid technologyId)
        => await _command.RemoveAsync(projectId, technologyId);

    public async Task RemoveAllByProjectIdAsync(Guid projectId)
        => await _command.RemoveAllByProjectIdAsync(projectId);

    public async Task<IEnumerable<Guid>> GetTechnologiesByProjectIdAsync(Guid projectId)
        => await _query.GetTechnologiesByProjectIdAsync(projectId);

    public async Task<IEnumerable<Guid>> GetProjectsByTechnologyIdAsync(Guid technologyId)
        => await _query.GetProjectsByTechnologyIdAsync(technologyId);

    public async Task<bool> ExistsAsync(Guid projectId, Guid technologyId)
        => await _query.ExistsAsync(projectId, technologyId);

    public async Task UpdateTechnologiesAsync(Guid projectId, List<Guid> technologyIds)
    {
        await _command.RemoveAllByProjectIdAsync(projectId);
        foreach (var techId in technologyIds)
        {
            await _command.AddAsync(projectId, techId);
        }
    }
}
