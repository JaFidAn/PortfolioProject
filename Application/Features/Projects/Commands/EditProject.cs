using Application.Core;
using Application.Features.Projects.DTOs;
using Application.Repositories.ProjectRepository;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Projects.Commands;

public class EditProject
{
    public class Command : IRequest<Result<Unit>>
    {
        public required EditProjectDto ProjectDto { get; set; }
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
                .GetWhere(p => p.Id == request.ProjectDto.Id && !p.IsDeleted)
                .Include(p => p.ProjectTechnologies)
                .FirstOrDefaultAsync(cancellationToken);

            if (project is null)
            {
                return Result<Unit>.Failure("Project not found", 404);
            }

            _projectWriteRepository.RemoveProjectTechnologies(project);

            foreach (var techId in request.ProjectDto.TechnologyIds)
            {
                project.ProjectTechnologies.Add(new ProjectTechnology
                {
                    ProjectId = project.Id,
                    TechnologyId = techId
                });
            }

            project.Title = request.ProjectDto.Title;
            project.Description = request.ProjectDto.Description;
            project.Link = request.ProjectDto.Link;

            var result = await _projectWriteRepository.SaveAsync() > 0;

            if (!result)
            {
                return Result<Unit>.Failure("Failed to update project", 400);
            }

            return Result<Unit>.Success(Unit.Value, "Project updated successfully");
        }
    }
}
