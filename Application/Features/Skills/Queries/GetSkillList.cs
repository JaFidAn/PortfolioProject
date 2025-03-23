using Application.Core;
using Application.Features.Skills.DTOs;
using Application.Repositories.SkillRepository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.Features.Skills.Queries;

public class GetSkillList
{
    public class Query : IRequest<PagedResult<SkillDto>>
    {
        public PaginationParams Params { get; set; } = new();
    }

    public class Handler : IRequestHandler<Query, PagedResult<SkillDto>>
    {
        private readonly ISkillReadRepository _skillReadRepository;
        private readonly IMapper _mapper;

        public Handler(ISkillReadRepository skillReadRepository, IMapper mapper)
        {
            _skillReadRepository = skillReadRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<SkillDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _skillReadRepository
                .GetWhere(s => !s.IsDeleted)
                .OrderBy(s => s.CreatedAt)
                .ProjectTo<SkillDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            return await PagedResult<SkillDto>.CreateAsync(
                query,
                request.Params.PageNumber,
                request.Params.PageSize,
                cancellationToken
            );
        }
    }
}
