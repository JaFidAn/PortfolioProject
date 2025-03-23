using Application.Repositories.ProjectRepository;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.ProjectRepository;

public class ProjectWriteRepository : WriteRepository<Project>, IProjectWriteRepository
{
    private readonly ApplicationDbContext _context;

    public ProjectWriteRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void RemoveProjectTechnologies(Project project)
    {
        _context.ProjectTechnologies.RemoveRange(project.ProjectTechnologies);
    }
}
