using Application.Core;
using Application.Features.Achievements.DTOs;
using Application.Repositories.AchievementRepository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.Features.Achievements.Queries;

public class GetAchievementList
{
    public class Query : IRequest<PagedResult<AchievementDto>>
    {
        public PaginationParams Params { get; set; } = new();
    }

    public class Handler : IRequestHandler<Query, PagedResult<AchievementDto>>
    {
        private readonly IAchievementReadRepository _achievementReadRepository;
        private readonly IMapper _mapper;

        public Handler(IAchievementReadRepository achievementReadRepository, IMapper mapper)
        {
            _achievementReadRepository = achievementReadRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<AchievementDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _achievementReadRepository
                .GetWhere(a => !a.IsDeleted)
                .OrderBy(a => a.CreatedAt)
                .ProjectTo<AchievementDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            return await PagedResult<AchievementDto>.CreateAsync(
                query,
                request.Params.PageNumber,
                request.Params.PageSize,
                cancellationToken
            );
        }
    }
}
