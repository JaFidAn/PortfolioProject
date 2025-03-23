using Application.Features.Projects.DTOs;
using Application.Repositories.ProjectRepository;
using AutoMapper;
using MediatR;
using AutoMapper.QueryableExtensions;
using Application.Core;

namespace Application.Features.Projects.Queries;

public class GetProjectList
{
    public class Query : IRequest<PagedResult<ProjectDto>>
    {
        public PaginationParams Params { get; set; } = new();
    }

    public class Handler : IRequestHandler<Query, PagedResult<ProjectDto>>
    {
        private readonly IProjectReadRepository _projectReadRepository;
        private readonly IMapper _mapper;

        public Handler(IProjectReadRepository projectReadRepository, IMapper mapper)
        {
            _projectReadRepository = projectReadRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ProjectDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _projectReadRepository
                .GetWhere(p => !p.IsDeleted)
                .OrderBy(p => p.CreatedAt)
                .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            return await PagedResult<ProjectDto>.CreateAsync(
                query,
                request.Params.PageNumber,
                request.Params.PageSize,
                cancellationToken
            );
        }
    }
}
