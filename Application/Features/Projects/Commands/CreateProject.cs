using Application.Core;
using Application.Features.Projects.DTOs;
using Application.Repositories.ProjectRepository;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Projects.Commands;

public class CreateProject
{
    public class Command : IRequest<Result<string>>
    {
        public CreateProjectDto ProjectDto { get; set; } = null!;
    }

    public class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly IProjectWriteRepository _projectWriteRepository;
        private readonly IMapper _mapper;

        public Handler(IProjectWriteRepository projectWriteRepository, IMapper mapper)
        {
            _projectWriteRepository = projectWriteRepository;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var project = _mapper.Map<Project>(request.ProjectDto);

            foreach (var techId in request.ProjectDto.TechnologyIds)
            {
                project.ProjectTechnologies.Add(new ProjectTechnology
                {
                    ProjectId = project.Id,
                    TechnologyId = techId
                });
            }

            await _projectWriteRepository.AddAsync(project);
            var result = await _projectWriteRepository.SaveAsync() > 0;

            if (!result)
            {
                return Result<string>.Failure("Project could not be created", 400);
            }

            return Result<string>.Success(project.Id, "Project created successfully");
        }
    }
}
