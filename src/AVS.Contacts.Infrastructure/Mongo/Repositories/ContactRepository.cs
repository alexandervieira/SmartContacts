using AVS.Contacts.Domain.Entities;
using AVS.Contacts.Domain.Repositories;
using AVS.Contacts.Infrastructure.Mongo.Configuration;
using Microsoft.Extensions.Options;

namespace AVS.Contacts.Infrastructure.Mongo.Repositories;

public class ContactRepository : MongoRepository<Contact, Guid>, IContactRepository
{
    public ContactRepository(IOptions<MongoSettings> settings)
        : base(settings, settings.Value.ContactsCollection)
    {
    }
}