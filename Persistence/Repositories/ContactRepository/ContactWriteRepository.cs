using Application.Repositories.ContactRepository;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.ContactRepository;

public class ContactWriteRepository : WriteRepository<Contact>, IContactWriteRepository
{
    public ContactWriteRepository(ApplicationDbContext context) : base(context)
    {
    }
}
