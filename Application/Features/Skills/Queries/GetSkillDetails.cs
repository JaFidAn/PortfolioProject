using Application.Core;
using Application.Features.Skills.DTOs;
using Application.Repositories.SkillRepository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Skills.Queries;

public class GetSkillDetails
{
    public class Query : IRequest<Result<SkillDto>>
    {
        public required string Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<SkillDto>>
    {
        private readonly ISkillReadRepository _skillReadRepository;
        private readonly IMapper _mapper;

        public Handler(ISkillReadRepository skillReadRepository, IMapper mapper)
        {
            _skillReadRepository = skillReadRepository;
            _mapper = mapper;
        }

        public async Task<Result<SkillDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var skill = await _skillReadRepository
                .GetWhere(s => !s.IsDeleted)
                .ProjectTo<SkillDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (skill is null)
            {
                return Result<SkillDto>.Failure("Skill not found", 404);
            }

            return Result<SkillDto>.Success(skill);
        }
    }
}
