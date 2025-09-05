namespace AVS.Contacts.Domain.ValueObjects
{
    public class Address
    {
        public string Street { get; }
        public string Number { get; }
        public string District { get; }
        public string City { get; }

        public Address(string street, string number, string district, string city)
        {
            Street = street;
            Number = number;
            District = district;
            City = city;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Address other) return false;
            return Street == other.Street && Number == other.Number && District == other.District && City == other.City;
        }

        public override int GetHashCode() => HashCode.Combine(Street, Number, District, City);
    }
}
