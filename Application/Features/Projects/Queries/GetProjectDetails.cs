using Application.Core;
using Application.Features.Projects.DTOs;
using Application.Repositories.ProjectRepository;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace Application.Features.Projects.Queries;

public class GetProjectDetails
{
    public class Query : IRequest<Result<ProjectDto>>
    {
        public required string Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<ProjectDto>>
    {
        private readonly IProjectReadRepository _projectReadRepository;
        private readonly IMapper _mapper;

        public Handler(IProjectReadRepository projectReadRepository, IMapper mapper)
        {
            _projectReadRepository = projectReadRepository;
            _mapper = mapper;
        }

        public async Task<Result<ProjectDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var project = await _projectReadRepository
                .GetWhere(p => !p.IsDeleted)
                .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (project is null)
            {
                return Result<ProjectDto>.Failure("Project not found", 404);
            }

            return Result<ProjectDto>.Success(project);
        }
    }
}
