using Application.Repositories.TechnologyRepository;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.TechnologyRepository;

public class TechnologyReadRepository : ReadRepository<Technology>, ITechnologyReadRepository
{
    public TechnologyReadRepository(ApplicationDbContext context) : base(context) { }
}
