using AVS.Contacts.Domain.ValueObjects;
using FluentAssertions;

namespace AVS.Contacts.Tests.Unit.ValueObjects
{
    public class PhoneNumberTests
    {
        [Fact]
        public void Constructor_ShouldSetProperties()
        {
            var phone = new PhoneNumber("+55", "11", "999999999");
            phone.CountryCode.Should().Be("+55");
            phone.AreaCode.Should().Be("11");
            phone.Number.Should().Be("999999999");
        }

        [Fact]
        public void Equals_ShouldReturnTrue_ForSameValues()
        {
            var p1 = new PhoneNumber("+55", "11", "999999999");
            var p2 = new PhoneNumber("+55", "11", "999999999");
            p1.Should().Be(p2);
        }

        [Fact]
        public void Equals_ShouldReturnFalse_ForDifferentValues()
        {
            var p1 = new PhoneNumber("+55", "11", "999999999");
            var p2 = new PhoneNumber("+1", "21", "888888888");
            p1.Should().NotBe(p2);
        }
    }
}
