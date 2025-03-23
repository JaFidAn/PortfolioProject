using Application.Core;
using Application.Features.Skills.DTOs;
using Application.Repositories.SkillRepository;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Skills.Commands;

public class CreateSkill
{
    public class Command : IRequest<Result<string>>
    {
        public CreateSkillDto SkillDto { get; set; } = null!;
    }

    public class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly ISkillWriteRepository _skillWriteRepository;
        private readonly ISkillReadRepository _skillReadRepository;
        private readonly IMapper _mapper;

        public Handler(
            ISkillWriteRepository skillWriteRepository,
            ISkillReadRepository skillReadRepository,
            IMapper mapper)
        {
            _skillWriteRepository = skillWriteRepository;
            _skillReadRepository = skillReadRepository;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var skill = _mapper.Map<Skill>(request.SkillDto);

            await _skillWriteRepository.AddAsync(skill);
            var result = await _skillWriteRepository.SaveAsync() > 0;

            if (!result)
            {
                return Result<string>.Failure("Skill could not be created", 400);
            }

            return Result<string>.Success(skill.Id, "Skill created successfully");
        }
    }
}
