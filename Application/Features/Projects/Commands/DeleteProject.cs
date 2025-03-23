using Application.Core;
using Application.Repositories.ProjectRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Projects.Commands;

public class DeleteProject
{
    public class Command : IRequest<Result<Unit>>
    {
        public required string Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IProjectReadRepository _projectReadRepository;
        private readonly IProjectWriteRepository _projectWriteRepository;

        public Handler(
            IProjectReadRepository projectReadRepository,
            IProjectWriteRepository projectWriteRepository)
        {
            _projectReadRepository = projectReadRepository;
            _projectWriteRepository = projectWriteRepository;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var project = await _projectReadRepository
                .GetWhere(p => p.Id == request.Id && !p.IsDeleted)
                .Include(p => p.ProjectTechnologies)
                .FirstOrDefaultAsync(cancellationToken);

            if (project is null)
            {
                return Result<Unit>.Failure("Project not found", 404);
            }

            _projectWriteRepository.RemoveProjectTechnologies(project);

            project.IsDeleted = true;

            var result = await _projectWriteRepository.SaveAsync() > 0;

            if (!result)
            {
                return Result<Unit>.Failure("Failed to delete project", 400);
            }

            return Result<Unit>.Success(Unit.Value, "Project deleted successfully");
        }
    }
}
