using Application.Core;
using Application.Features.Technologies.DTOs;
using Application.Repositories.TechnologyRepository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Technologies.Commands;

public class CreateTechnology
{
    public class Command : IRequest<Result<string>>
    {
        public CreateTechnologyDto TechnologyDto { get; set; } = null!;
    }

    public class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly ITechnologyWriteRepository _technologyWriteRepository;
        private readonly ITechnologyReadRepository _technologyReadRepository;
        private readonly IMapper _mapper;

        public Handler(
            ITechnologyWriteRepository technologyWriteRepository,
            ITechnologyReadRepository technologyReadRepository,
            IMapper mapper)
        {
            _technologyWriteRepository = technologyWriteRepository;
            _technologyReadRepository = technologyReadRepository;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var exists = await _technologyReadRepository
                .GetWhere(t => t.Name.ToLower() == request.TechnologyDto.Name.ToLower() && !t.IsDeleted)
                .AnyAsync(cancellationToken);

            if (exists)
            {
                return Result<string>.Failure("Technology name already exists.", 400);
            }

            var technology = _mapper.Map<Technology>(request.TechnologyDto);

            await _technologyWriteRepository.AddAsync(technology);
            var result = await _technologyWriteRepository.SaveAsync() > 0;

            if (!result)
            {
                return Result<string>.Failure("Technology could not be created", 400);
            }

            return Result<string>.Success(technology.Id, "Technology created successfully");
        }
    }
}
