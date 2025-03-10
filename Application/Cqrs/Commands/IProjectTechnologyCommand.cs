namespace Application.Cqrs.Commands;

public interface IProjectTechnologyCommand
{
    Task AddAsync(Guid projectId, Guid technologyId);
    Task RemoveAsync(Guid projectId, Guid technologyId);
    Task RemoveAllByProjectIdAsync(Guid projectId);
}
