using Application.Repositories.AchievementRepository;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.AchievementRepository;

public class AchievementReadRepository : ReadRepository<Achievement>, IAchievementReadRepository
{
    public AchievementReadRepository(ApplicationDbContext context) : base(context) { }
}
