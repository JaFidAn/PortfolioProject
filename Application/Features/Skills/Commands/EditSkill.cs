using Application.Core;
using Application.Features.Skills.DTOs;
using Application.Repositories.SkillRepository;
using MediatR;

namespace Application.Features.Skills.Commands;

public class EditSkill
{
    public class Command : IRequest<Result<Unit>>
    {
        public required EditSkillDto SkillDto { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly ISkillReadRepository _skillReadRepository;
        private readonly ISkillWriteRepository _skillWriteRepository;

        public Handler(
            ISkillReadRepository skillReadRepository,
            ISkillWriteRepository skillWriteRepository)
        {
            _skillReadRepository = skillReadRepository;
            _skillWriteRepository = skillWriteRepository;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var skill = await _skillReadRepository.GetByIdAsync(request.SkillDto.Id);

            if (skill is null)
            {
                return Result<Unit>.Failure("Skill not found", 404);
            }

            skill.Name = request.SkillDto.Name;
            skill.Level = request.SkillDto.Level;

            var result = await _skillWriteRepository.SaveAsync() > 0;

            if (!result)
            {
                return Result<Unit>.Failure("Failed to update skill", 400);
            }

            return Result<Unit>.Success(Unit.Value, "Skill updated successfully");
        }
    }
}
