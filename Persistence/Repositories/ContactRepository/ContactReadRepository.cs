using Application.Repositories.ContactRepository;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.ContactRepository;

public class ContactReadRepository : ReadRepository<Contact>, IContactReadRepository
{
    public ContactReadRepository(ApplicationDbContext context) : base(context)
    {
    }
}
