using Application.Repository.ContactRepository;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repository.ContactRepository
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
