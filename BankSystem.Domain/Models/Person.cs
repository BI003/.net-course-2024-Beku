namespace BankSystem.Domain.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public int Passport { get; set; }
        public int PhoneNumber {  get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
