namespace AVS.Contacts.Domain.ValueObjects
{
    public class Name
    {
        public string FirstName { get; }
        public string LastName { get; }

        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Name other) return false;
            return FirstName == other.FirstName && LastName == other.LastName;
        }

        public override int GetHashCode() => HashCode.Combine(FirstName, LastName);
    }
}
