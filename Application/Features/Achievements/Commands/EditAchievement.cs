using Application.Core;
using Application.Features.Achievements.DTOs;
using Application.Repositories.AchievementRepository;
using MediatR;

namespace Application.Features.Achievements.Commands;

public class EditAchievement
{
    public class Command : IRequest<Result<Unit>>
    {
        public required EditAchievementDto AchievementDto { get; set; }
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
            var achievement = await _achievementReadRepository.GetByIdAsync(request.AchievementDto.Id);

            if (achievement is null)
            {
                return Result<Unit>.Failure("Achievement not found", 404);
            }

            achievement.Title = request.AchievementDto.Title;
            achievement.Description = request.AchievementDto.Description;

            var result = await _achievementWriteRepository.SaveAsync() > 0;

            if (!result)
            {
                return Result<Unit>.Failure("Failed to update achievement", 400);
            }

            return Result<Unit>.Success(Unit.Value, "Achievement updated successfully");
        }
    }
}
