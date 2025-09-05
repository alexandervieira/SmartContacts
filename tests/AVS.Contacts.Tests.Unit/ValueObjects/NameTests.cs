using AVS.Contacts.Domain.ValueObjects;
using FluentAssertions;

namespace AVS.Contacts.Tests.Unit.ValueObjects
{
    public class NameTests
    {
        [Fact]
        public void Constructor_ShouldSetProperties()
        {
            var name = new Name("Jo�o", "Silva");
            name.FirstName.Should().Be("Jo�o");
            name.LastName.Should().Be("Silva");
        }

        [Fact]
        public void Equals_ShouldReturnTrue_ForSameValues()
        {
            var n1 = new Name("Jo�o", "Silva");
            var n2 = new Name("Jo�o", "Silva");
            n1.Should().Be(n2);
        }

        [Fact]
        public void Equals_ShouldReturnFalse_ForDifferentValues()
        {
            var n1 = new Name("Jo�o", "Silva");
            var n2 = new Name("Maria", "Oliveira");
            n1.Should().NotBe(n2);
        }
    }
}
