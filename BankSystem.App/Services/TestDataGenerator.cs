using BankSystem.Domain.Models;
using Bogus;
namespace BankSystem.App.Services;
using System.Linq;

public class TestDataGenerator
{
    public List<Client> GenerateClients()
    {
        var clients = new List<Client>();
        var faker = new Faker<Client>("ru")
            .RuleFor(c => c.Name, f => f.Name.FirstName())
            .RuleFor(c => c.Surname, f => f.Name.LastName())
            .RuleFor(c => c.PhoneNumber, f => int.Parse(f.Phone.PhoneNumber("373####")))
            .RuleFor(c => c.Age, f => f.Random.Int(18,50));

        while (clients.Count < 1000) 
        {
            var newClient = faker.Generate();

            if (!clients.Any(c => c.PhoneNumber == newClient.PhoneNumber))
            {
                clients.Add(newClient);
            }
        }
        return clients;
    }

    public Dictionary<int, Client> GenerateClientsDictionaryFromList(List<Client> clients)
    {
        var clientDictionary = clients.ToDictionary(c => c.PhoneNumber, c => c);
        return clientDictionary;
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
