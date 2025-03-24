using Application.Core;
using Application.Repositories.AchievementRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Achievements.Commands;

public class DeleteAchievement
{
    public class Command : IRequest<Result<Unit>>
    {
        public required string Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IAchievementReadRepository _achievementReadRepository;
        private readonly IAchievementWriteRepository _achievementWriteRepository;

        public Handler(
            IAchievementReadRepository achievementReadRepository,
            IAchievementWriteRepository achievementWriteRepository)
        {
            _achievementReadRepository = achievementReadRepository;
            _achievementWriteRepository = achievementWriteRepository;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var achievement = await _achievementReadRepository
                .GetWhere(a => a.Id == request.Id && !a.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);

            if (achievement is null)
            {
                return Result<Unit>.Failure("Achievement not found", 404);
            }

            achievement.IsDeleted = true;

            var result = await _achievementWriteRepository.SaveAsync() > 0;

            if (!result)
            {
                return Result<Unit>.Failure("Failed to delete achievement", 400);
            }

            return Result<Unit>.Success(Unit.Value, "Achievement deleted successfully");
        }
    }
}
