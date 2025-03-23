using Application.Repositories.SkillRepository;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.SkillRepository;

public class SkillReadRepository : ReadRepository<Skill>, ISkillReadRepository
{
    public SkillReadRepository(ApplicationDbContext context) : base(context)
    {
    }
}
