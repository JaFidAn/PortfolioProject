using Application.Core;
using Application.Features.Technologies.DTOs;
using Application.Repositories.TechnologyRepository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.Features.Technologies.Queries;

public class GetTechnologyList
{
    public class Query : IRequest<PagedResult<TechnologyDto>>
    {
        public PaginationParams Params { get; set; } = new();
    }

    public class Handler : IRequestHandler<Query, PagedResult<TechnologyDto>>
    {
        private readonly ITechnologyReadRepository _technologyReadRepository;
        private readonly IMapper _mapper;

        public Handler(ITechnologyReadRepository technologyReadRepository, IMapper mapper)
        {
            _technologyReadRepository = technologyReadRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<TechnologyDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _technologyReadRepository
                .GetWhere(t => !t.IsDeleted)
                .OrderBy(p => p.CreatedAt)
                .ProjectTo<TechnologyDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            return await PagedResult<TechnologyDto>.CreateAsync(
                query,
                request.Params.PageNumber,
                request.Params.PageSize,
                cancellationToken
            );
        }
    }
}
