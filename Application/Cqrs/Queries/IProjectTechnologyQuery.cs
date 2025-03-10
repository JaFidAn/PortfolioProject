namespace Application.Cqrs.Queries;

public interface IProjectTechnologyQuery
{
    Task<IEnumerable<Guid>> GetTechnologiesByProjectIdAsync(Guid projectId);
    Task<IEnumerable<Guid>> GetProjectsByTechnologyIdAsync(Guid technologyId);
    Task<bool> ExistsAsync(Guid projectId, Guid technologyId);
}
