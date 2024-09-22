namespace BankSystem.Domain.Models
{
    public class Employee : Person
    {
        public string Contract { get; set; }

        public Employee(string name, string surname, int passport, int number) : base(name, surname, passport, number)
        {
        }
    }
}
