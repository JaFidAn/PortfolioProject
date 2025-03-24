using Application.Repositories.AchievementRepository;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.AchievementRepository;

public class AchievementWriteRepository : WriteRepository<Achievement>, IAchievementWriteRepository
{
    public AchievementWriteRepository(ApplicationDbContext context) : base(context) { }
}
