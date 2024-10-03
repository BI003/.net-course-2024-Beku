namespace BankSystem.Domain.Models
{
    public class Client : Person
    {
        public override bool Equals(object obj)
        {
            if (obj is Client other)
            {
                return this.Name == other.Name &&
                       this.Surname == other.Surname &&
                       this.Age == other.Age &&    
                       this.Passport == other.Passport;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Surname, Age, Passport);
        }
    }
}
