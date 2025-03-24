using Application.Core;
using Application.Features.Contacts.DTOs;
using Application.Repositories.ContactRepository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.Features.Contacts.Queries;

public class GetContactList
{
    public class Query : IRequest<PagedResult<ContactDto>>
    {
        public PaginationParams Params { get; set; } = new();
    }

    public class Handler : IRequestHandler<Query, PagedResult<ContactDto>>
    {
        private readonly IContactReadRepository _contactReadRepository;
        private readonly IMapper _mapper;

        public Handler(IContactReadRepository contactReadRepository, IMapper mapper)
        {
            _contactReadRepository = contactReadRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ContactDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _contactReadRepository
                .GetWhere(c => !c.IsDeleted)
                .OrderByDescending(c => c.CreatedAt)
                .ProjectTo<ContactDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            return await PagedResult<ContactDto>.CreateAsync(
                query,
                request.Params.PageNumber,
                request.Params.PageSize,
                cancellationToken
            );
        }
    }
}
