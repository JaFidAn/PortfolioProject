using Application.Core;
using Application.Features.Achievements.DTOs;
using Application.Repositories.AchievementRepository;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Achievements.Commands;

public class CreateAchievement
{
    public class Command : IRequest<Result<string>>
    {
        public CreateAchievementDto AchievementDto { get; set; } = null!;
    }

    public class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly IAchievementWriteRepository _achievementWriteRepository;
        private readonly IMapper _mapper;

        public Handler(
            IAchievementWriteRepository achievementWriteRepository,
            IMapper mapper)
        {
            _achievementWriteRepository = achievementWriteRepository;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var achievement = _mapper.Map<Achievement>(request.AchievementDto);

            await _achievementWriteRepository.AddAsync(achievement);
            var result = await _achievementWriteRepository.SaveAsync() > 0;

            if (!result)
            {
                return Result<string>.Failure("Achievement could not be created", 400);
            }

            return Result<string>.Success(achievement.Id, "Achievement recorded successfully");
        }
    }
}
