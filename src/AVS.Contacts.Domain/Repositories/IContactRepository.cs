using AVS.Contacts.Domain.Entities;

namespace AVS.Contacts.Domain.Repositories
{
    public interface IContactRepository : IRepository<Contact, Guid>
    {
        // M�todos espec�ficos para Contact podem ser adicionados aqui
    }
}
