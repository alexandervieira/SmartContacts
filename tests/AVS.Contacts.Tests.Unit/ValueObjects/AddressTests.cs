using AVS.Contacts.Domain.ValueObjects;
using FluentAssertions;

namespace AVS.Contacts.Tests.Unit.ValueObjects
{
    public class AddressTests
    {
        [Fact]
        public void Constructor_ShouldSetProperties()
        {
            var address = new Address("Rua X", "123", "Centro", "São Paulo");
            address.Street.Should().Be("Rua X");
            address.Number.Should().Be("123");
            address.District.Should().Be("Centro");
            address.City.Should().Be("São Paulo");
        }

        [Fact]
        public void Equals_ShouldReturnTrue_ForSameValues()
        {
            var a1 = new Address("Rua X", "123", "Centro", "São Paulo");
            var a2 = new Address("Rua X", "123", "Centro", "São Paulo");
            a1.Should().Be(a2);
        }

        [Fact]
        public void Equals_ShouldReturnFalse_ForDifferentValues()
        {
            var a1 = new Address("Rua X", "123", "Centro", "São Paulo");
            var a2 = new Address("Rua Y", "456", "Bairro", "Rio de Janeiro");
            a1.Should().NotBe(a2);
        }
    }
}
