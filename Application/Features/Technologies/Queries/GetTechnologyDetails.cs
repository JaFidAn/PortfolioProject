using Application.Core;
using Application.Features.Technologies.DTOs;
using Application.Repositories.TechnologyRepository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Technologies.Queries;

public class GetTechnologyDetails
{
    public class Query : IRequest<Result<TechnologyDto>>
    {
        public required string Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<TechnologyDto>>
    {
        private readonly ITechnologyReadRepository _technologyReadRepository;
        private readonly IMapper _mapper;

        public Handler(ITechnologyReadRepository technologyReadRepository, IMapper mapper)
        {
            _technologyReadRepository = technologyReadRepository;
            _mapper = mapper;
        }

        public async Task<Result<TechnologyDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var technology = await _technologyReadRepository
                .GetWhere(t => !t.IsDeleted)
                .ProjectTo<TechnologyDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (technology is null)
            {
                return Result<TechnologyDto>.Failure("Technology not found", 404);
            }

            return Result<TechnologyDto>.Success(technology);
        }
    }
}
