using AVS.Contacts.Domain.Entities;

namespace AVS.Contacts.Domain.Repositories
{
    public interface IContactRepository : IRepository<Contact, Guid>
    {
        // Métodos específicos para Contact podem ser adicionados aqui
    }
}
