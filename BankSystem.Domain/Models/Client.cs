namespace BankSystem.Domain.Models
{
    public class Client : Person
    {
        public int Age { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Client other)
            {
                return this.Name == other.Name && this.Surname == other.Surname;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Surname);
        }
    }
}
