using Application.Repositories.TechnologyRepository;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.TechnologyRepository;

public class TechnologyWriteRepository : WriteRepository<Technology>, ITechnologyWriteRepository
{
    public TechnologyWriteRepository(ApplicationDbContext context) : base(context) { }
}
