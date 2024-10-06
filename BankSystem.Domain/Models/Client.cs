namespace BankSystem.Domain.Models
{
    public class Client : Person
    {
        public Dictionary<string, List<Account>> Accounts { get; set; }

        public Client()
        {
            Accounts = new Dictionary<string, List<Account>>();
        }

        public override bool Equals(object obj)
        {
            if (obj is Client other)
            {
                return this.Name == other.Name &&
                       this.Surname == other.Surname &&
                       this.Age == other.Age &&
                       this.PhoneNumber == other.PhoneNumber &&
                       this.Passport == other.Passport &&
                       this.DateOfBirth == other.DateOfBirth;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Surname, Age, Passport, PhoneNumber, DateOfBirth);
        }
    }
}
