using Application.Core;
using Application.Features.Achievements.DTOs;
using Application.Repositories.AchievementRepository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Achievements.Queries;

public class GetAchievementDetails
{
    public class Query : IRequest<Result<AchievementDto>>
    {
        public required string Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<AchievementDto>>
    {
        private readonly IAchievementReadRepository _achievementReadRepository;
        private readonly IMapper _mapper;

        public Handler(IAchievementReadRepository achievementReadRepository, IMapper mapper)
        {
            _achievementReadRepository = achievementReadRepository;
            _mapper = mapper;
        }

        public async Task<Result<AchievementDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var achievement = await _achievementReadRepository
                .GetWhere(a => !a.IsDeleted)
                .ProjectTo<AchievementDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (achievement is null)
            {
                return Result<AchievementDto>.Failure("Achievement not found", 404);
            }

            return Result<AchievementDto>.Success(achievement);
        }
    }
}
