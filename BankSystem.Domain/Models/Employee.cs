namespace BankSystem.Domain.Models
{
    public class Employee : Person
    {
        public string Contract { get; set; }
        public bool IsOwner { get; set; } = false;

        public Employee(string name, string surname, int passport, int number, bool isOwner = false) : base(name, surname, passport, number)
        {
            IsOwner = isOwner;
        }
    }
}
