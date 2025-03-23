using Application.Repositories.SkillRepository;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.SkillRepository;

public class SkillWriteRepository : WriteRepository<Skill>, ISkillWriteRepository
{
    public SkillWriteRepository(ApplicationDbContext context) : base(context)
    {
    }
}
