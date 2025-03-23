using Domain.Entities;

namespace Application.Repositories.ProjectRepository;

public interface IProjectWriteRepository : IWriteRepository<Project>
{
    void RemoveProjectTechnologies(Project project);
}
