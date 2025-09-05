using AVS.Contacts.Domain.Common;
using AVS.Contacts.Domain.ValueObjects;

namespace AVS.Contacts.Domain.Entities;

public class Contact : BaseEntity<Guid>
{
    public Name Name { get; private set; }
    public Address Address { get; private set; }
    public PhoneNumber Phone { get; private set; }

    public Contact(Name name, Address address, PhoneNumber phone)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Address = address ?? throw new ArgumentNullException(nameof(address));
        Phone = phone ?? throw new ArgumentNullException(nameof(phone));
    }

    public void UpdateContact(Name? name = null, Address? address = null, PhoneNumber? phone = null)
    {
        if (name is not null) Name = name;
        if (address is not null) Address = address;
        if (phone is not null) Phone = phone;
        UpdateTimestamp();
    }
}