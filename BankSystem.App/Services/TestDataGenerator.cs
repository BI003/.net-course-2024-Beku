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

    public Dictionary<Client, List<Account>> GenerateDictionaryClientsAndAccounts()
    {
        var clientsAndAccounts = new Dictionary<Client, List<Account>>();
        var fakerClient = new Faker<Client>("ru")
            .RuleFor(c => c.Name, f => f.Name.FirstName())
            .RuleFor(c => c.Surname, f => f.Name.LastName())
            .RuleFor(c => c.Passport, f => f.Random.Int(1111111, 9999999))
            .RuleFor(c => c.Age, f => f.Random.Int(18, 50));

        var fakerAccount = new Faker<Account>()
       .RuleFor(a => a.CurrencyName, f => f.Finance.Currency().Code)
       .RuleFor(a => a.Amount, f => f.Finance.Amount(10000, 100000));

        while (clientsAndAccounts.Count < 1000)
        {
            var client = fakerClient.Generate();

            var accountList = new List<Account>();
            var accountCount = new Random().Next(1,3);
            for (int i = 0; i < accountCount; i++)
            {
                var account = fakerAccount.Generate();
                accountList.Add(account);
            }

            if (!clientsAndAccounts.Keys.Any(c => c.Equals(client)))
            {
                clientsAndAccounts.Add(client, accountList);
            }
        }
        return clientsAndAccounts;
    }

    public List<Employee> GenerateEmployeeClientsAndAccounts()
    {
        var employees = new List<Employee>();
        var faker = new Faker<Employee>("ru")
            .RuleFor(e => e.Name, f => f.Name.FirstName())
            .RuleFor(e => e.Surname, f => f.Name.LastName())
            .RuleFor(e => e.Contract, f => f.Random.AlphaNumeric(10))
            .RuleFor(e => e.Salary, f => f.Random.Int(25000, 50000))
            .RuleFor(c => c.Passport, f => f.Random.Int(1111111, 9999999));

        while (employees.Count < 1000)
        {
            var employee = faker.Generate();
            if (!employees.Any(e => e.Equals(employee)))
            {
                employees.Add(employee);
            }
        }
        return employees;
    }
}
