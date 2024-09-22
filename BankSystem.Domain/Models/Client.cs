namespace BankSystem.Domain.Models
{
    public class Client : Person
    {
        public Client(string name, string surname, int passport, int number) : base(name, surname, passport, number)
        {
        }
    }
}
