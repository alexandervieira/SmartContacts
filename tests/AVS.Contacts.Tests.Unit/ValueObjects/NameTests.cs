using AVS.Contacts.Domain.ValueObjects;
using FluentAssertions;

namespace AVS.Contacts.Tests.Unit.ValueObjects
{
    public class NameTests
    {
        [Fact]
        public void Constructor_ShouldSetProperties()
        {
            var name = new Name("João", "Silva");
            name.FirstName.Should().Be("João");
            name.LastName.Should().Be("Silva");
        }

        [Fact]
        public void Equals_ShouldReturnTrue_ForSameValues()
        {
            var n1 = new Name("João", "Silva");
            var n2 = new Name("João", "Silva");
            n1.Should().Be(n2);
        }

        [Fact]
        public void Equals_ShouldReturnFalse_ForDifferentValues()
        {
            var n1 = new Name("João", "Silva");
            var n2 = new Name("Maria", "Oliveira");
            n1.Should().NotBe(n2);
        }
    }
}
