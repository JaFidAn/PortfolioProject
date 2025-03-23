using Application.Core;
using Application.Repositories.TechnologyRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Technologies.Commands;

public class DeleteTechnology
{
    public class Command : IRequest<Result<Unit>>
    {
        public required string Id { get; set; }
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
                .GetWhere(t => t.Id == request.Id && !t.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);

            if (technology is null)
            {
                return Result<Unit>.Failure("Technology not found", 404);
            }

            technology.IsDeleted = true;

            var result = await _technologyWriteRepository.SaveAsync() > 0;

            if (!result)
            {
                return Result<Unit>.Failure("Failed to delete technology", 400);
            }

            return Result<Unit>.Success(Unit.Value, "Technology deleted successfully");
        }
    }
}
