using AVS.Contacts.Domain.Entities;
using AVS.Contacts.Domain.ValueObjects;
using FluentAssertions;

namespace AVS.Contacts.Tests.Unit.Entities
{
    public class ContactTests
    {
        [Fact]
        public void Constructor_ShouldSetProperties()
        {
            var name = new Name("Jo�o", "Silva");
            var address = new Address("Rua X", "123", "Centro", "S�o Paulo");
            var phone = new PhoneNumber("+55", "11", "999999999");
            var contact = new Contact(name, address, phone);

            contact.Name.Should().Be(name);
            contact.Address.Should().Be(address);
            contact.Phone.Should().Be(phone);
        }
    }
}
