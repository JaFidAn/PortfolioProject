using Application.Core;
using Application.Features.Technologies.DTOs;
using Application.Repositories.TechnologyRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Technologies.Commands;

public class EditTechnology
{
    public class Command : IRequest<Result<Unit>>
    {
        public required EditTechnologyDto TechnologyDto { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly ITechnologyReadRepository _technologyReadRepository;
        private readonly ITechnologyWriteRepository _technologyWriteRepository;

        public Handler(
            ITechnologyReadRepository technologyReadRepository,
            ITechnologyWriteRepository technologyWriteRepository)
        {
            _technologyReadRepository = technologyReadRepository;
            _technologyWriteRepository = technologyWriteRepository;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var technology = await _technologyReadRepository
                .GetByIdAsync(request.TechnologyDto.Id);

            if (technology is null)
            {
                return Result<Unit>.Failure("Technology not found", 404);
            }

            var duplicate = await _technologyReadRepository
                .GetWhere(t => t.Id != request.TechnologyDto.Id &&
                               t.Name.ToLower() == request.TechnologyDto.Name.ToLower() &&
                               !t.IsDeleted)
                .AnyAsync(cancellationToken);

            if (duplicate)
            {
                return Result<Unit>.Failure("Another technology with the same name already exists.", 400);
            }

            technology.Name = request.TechnologyDto.Name;

            var result = await _technologyWriteRepository.SaveAsync() > 0;

            if (!result)
            {
                return Result<Unit>.Failure("Failed to update technology", 400);
            }

            return Result<Unit>.Success(Unit.Value, "Technology updated successfully");
        }
    }
}
