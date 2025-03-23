using Application.Repositories.ProjectRepository;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.ProjectRepository;

public class ProjectReadRepository : ReadRepository<Project>, IProjectReadRepository
{
    public ProjectReadRepository(ApplicationDbContext context) : base(context)
    {
    }
}
