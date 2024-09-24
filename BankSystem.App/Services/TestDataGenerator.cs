using BankSystem.Domain.Models;
using Bogus;
namespace BankSystem.App.Services
{
    public class TestDataGenerator
    {
        public List<Client> GenerateClients()
        {
            var clients = new List<Client>();
            var faker = new Faker<Client>("ru")
                .RuleFor(c => c.Name, f => f.Name.FirstName())
                .RuleFor(c => c.Surname, f => f.Name.LastName())
                .RuleFor(c => c.Number, f => f.Random.Int(3731000, 3732000))
                .RuleFor(c => c.Age, f => f.Random.Int(18, 50));

            clients.AddRange(faker.Generate(1000));
            return clients;
        }
        public Dictionary<int, Client> GenerateClientsDictionary()
        {
            var clientDictionary = new Dictionary<int, Client>();
            var faker = new Faker<Client>("ru")
                .RuleFor(c => c.Name, f => f.Name.FirstName())
                .RuleFor(c => c.Surname, f => f.Name.LastName())
                .RuleFor(c => c.Number, f => f.Random.Int(3731000, 3732000))
                .RuleFor(c => c.Age, f => f.Random.Int(18, 50));

            for (int i = 0; i < 1000; i++)
            {
                var client = faker.Generate();
                if (!clientDictionary.ContainsKey(client.Number))
                {
                    clientDictionary[client.Number] = client;
                }
            }
            return clientDictionary;
        }
        public List<Client> FindYoungerClients(List<Client> clients, int limitAge)
        {
            return clients.FindAll(client => client.Age < limitAge);
        }
        public List<Employee> GenerateEmployees()
        {
            var employees = new List<Employee>();
            var faker = new Faker<Employee>("ru")
                .RuleFor(c => c.Name, f => f.Name.FirstName())
                .RuleFor(c => c.Surname, f => f.Name.LastName())
                .RuleFor(e => e.Salary, f => f.Random.Int(25000, 50000));
            employees.AddRange(faker.Generate(1000));
            return employees;
        }
        public Employee FindEmployeeWithMinSalary(List<Employee> employees)
        {
            var employeeWithMinSalary = employees.MinBy(e => e.Salary);
            return employeeWithMinSalary;
        }
    }
}
