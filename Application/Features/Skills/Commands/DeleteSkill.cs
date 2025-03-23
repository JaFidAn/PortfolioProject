using Application.Core;
using Application.Repositories.SkillRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Skills.Commands;

public class DeleteSkill
{
    public class Command : IRequest<Result<Unit>>
    {
        public required string Id { get; set; }
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
            var skill = await _skillReadRepository
                .GetWhere(s => s.Id == request.Id && !s.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);

            if (skill is null)
            {
                return Result<Unit>.Failure("Skill not found", 404);
            }

            skill.IsDeleted = true;

            var result = await _skillWriteRepository.SaveAsync() > 0;

            if (!result)
            {
                return Result<Unit>.Failure("Failed to delete skill", 400);
            }

            return Result<Unit>.Success(Unit.Value, "Skill deleted successfully");
        }
    }
}
