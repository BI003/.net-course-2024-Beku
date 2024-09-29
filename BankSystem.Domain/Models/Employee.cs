namespace BankSystem.Domain.Models
{
    public class Employee : Person
    {
        public string Contract { get; set; }
        public int Salary { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Employee other)
            {
                return this.Name == other.Name &&
                       this.Surname == other.Surname &&
                       this.Contract == other.Contract &&
                       this.Salary == other.Salary &&
                       this.Passport == other.Passport;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Surname, Contract, Contract, Salary, Passport);
        }
    }
}
