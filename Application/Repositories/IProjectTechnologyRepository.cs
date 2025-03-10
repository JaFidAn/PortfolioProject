namespace Application.Repositories;

public interface IProjectTechnologyRepository
{
    Task AddAsync(Guid projectId, Guid technologyId);
    Task RemoveAsync(Guid projectId, Guid technologyId);
    Task RemoveAllByProjectIdAsync(Guid projectId);
    Task<IEnumerable<Guid>> GetTechnologiesByProjectIdAsync(Guid projectId);
    Task<IEnumerable<Guid>> GetProjectsByTechnologyIdAsync(Guid technologyId);
    Task<bool> ExistsAsync(Guid projectId, Guid technologyId);
    Task UpdateTechnologiesAsync(Guid projectId, List<Guid> technologyIds);
}
