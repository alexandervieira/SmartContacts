namespace AVS.Contacts.Domain.ValueObjects
{
    public class PhoneNumber
    {
        public string CountryCode { get; }
        public string AreaCode { get; }
        public string Number { get; }

        public PhoneNumber(string countryCode, string areaCode, string number)
        {
            CountryCode = countryCode;
            AreaCode = areaCode;
            Number = number;
        }

        public static PhoneNumber Create(string raw)
        {
            // Normaliza “+55 (11) 99999-9999” -> country=55, area=11, number=999999999
            var digits = new string(raw.Where(char.IsDigit).ToArray());
            if (digits.Length < 10) throw new ArgumentException("Telefone inválido");
            var country = digits.Length > 11 ? digits[..2] : "55";
            var area = digits.Length > 11 ? digits.Substring(2, 2) : digits[..2];
            var number = digits.Length > 11 ? digits[4..] : digits[2..];
            return new PhoneNumber(country, area, number);
        }

        public bool Equals(PhoneNumber? other) =>
            other is not null && CountryCode == other.CountryCode && AreaCode == other.AreaCode && Number == other.Number;

        public override string ToString() => $"+{CountryCode} ({AreaCode}) {Number}";

        public override int GetHashCode() => HashCode.Combine(CountryCode, AreaCode, Number);
    }
}
