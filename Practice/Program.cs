using BankSystem.App.Services;
using BankSystem.Domain.Models;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        var employee = new Employee();
        employee.Name = "Иван";
        employee.Surname = "Беку";
        employee.Passport = 123;
        employee.Number = 37360125;
        Console.WriteLine($"До: {employee.Contract}");
        var newContract = "Контракт создан!";
        CreateContract(employee, newContract);
        Console.WriteLine($"После: {employee.Contract}");

        var currency = new Currency();
        currency.Name = "Доллары";
        currency.Price = 10;
        Console.WriteLine($"До: {currency.Price}");
        var newPrice = 20;
        Update(ref currency, newPrice);
        Console.WriteLine($"После: {currency.Price}");

        var bankService = new BankService(10000, 2000, 2);
        var ownerSalary = BankService.OwnerSalaryCalculation(bankService.Profit, bankService.Expenses, bankService.NumberOwners);
        BankService.OwnerSalaryCalculation(bankService.Profit, bankService.Expenses, bankService.NumberOwners);
        Console.WriteLine($"Владельцев: {bankService.NumberOwners}");
        Console.WriteLine($"Зарплата каждого владельца: {ownerSalary}");

        var newclient = new Client();
        newclient.Name = "Дмитрий";
        newclient.Surname = "Дмитриевич";
        newclient.Passport = 987654;
        newclient.Number = 373898989;
        Console.WriteLine($"Клиент банка: {newclient.Name}, {newclient.Surname}, {newclient.Passport}, {newclient.Number}");
        var newContract2 = " Клиент стал Сотрудником";
        var newEmployee = BankService.ConvertClientInEmployee(newclient, newContract2);
        Console.WriteLine($"Сотрудник: {newEmployee.Name}, {newEmployee.Surname}, Контракт: {newEmployee.Contract}");

        Console.ReadLine();

        static void CreateContract(Employee employee, string contract)
        {
            employee.Contract = contract;
            Console.WriteLine("Создаётся контракт!");
        }

        static void Update(ref Currency currency, int price)
        {
            currency.Price = price;
            Console.WriteLine("Свойства изменяются!");
        }

        var generator = new TestDataGenerator();

        var clients = generator.GenerateClients();
        var clientDictionary = generator.GenerateClientsDictionary();
        var employees = generator.GenerateEmployees();

        Console.WriteLine("Поиск клиента по номеру");
        var searchByNumber = 3731795;

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var foundClient = clients.FirstOrDefault(client => client.Number == searchByNumber);
        if (foundClient != null)
        {
            Console.WriteLine($"Клиент найден в списке: {foundClient.Name} {foundClient.Surname}, Номер: {foundClient.Number}, Время поиска: {stopwatch.Elapsed} миллисекунд");
        }
        else
        {
            Console.WriteLine("Клиент не найден в списке.");
        }
        stopwatch.Reset();

        stopwatch.Start();
        var clientExists = clientDictionary.TryGetValue(searchByNumber, out var foundClient2);
        if (clientExists && foundClient2 != null)
        {
            Console.WriteLine($"Клиент найден в словаре: {foundClient2.Name} {foundClient2.Surname}, Номер: {foundClient2.Number}, Время поиска по ключу: {stopwatch.Elapsed} миллисекунд");
        }
        else
        {
            Console.WriteLine("Клиент не найден в словаре.");
        }
        stopwatch.Reset();

        stopwatch.Start();
        var lastClientInDictionary = clientDictionary.Values.LastOrDefault();
        if (lastClientInDictionary != null)
        {
            Console.WriteLine($"Последний клиент в словаре: {lastClientInDictionary.Name} {lastClientInDictionary.Surname}, Номер: {lastClientInDictionary.Number}, Время поиска последнего клиента: {stopwatch.Elapsed} миллисекунд");
        }
        else
        {
            Console.WriteLine("Словарь клиентов пуст.");
        }
        stopwatch.Stop();
        
        var limitAge = 19;
        var youngClients = generator.FindYoungerClients(clients, limitAge);
        Console.WriteLine($"Клиенты младше {limitAge} лет: {youngClients.Count} клиентов");
        foreach (var client in youngClients)
        {
            Console.WriteLine($"{client.Name} {client.Surname}, Возраст: {client.Age}");
        }

        var employeeWithMinSalary = generator.FindEmployeeWithMinSalary(employees);
        if (employeeWithMinSalary != null)
        {
            Console.WriteLine($"Сотрудник с минимальной зарплатой: {employeeWithMinSalary.Name} {employeeWithMinSalary.Surname}, Зарплата: {employeeWithMinSalary.Salary}");
        }
        else
        {
            Console.WriteLine("Сотрудники не найдены.");
        }
    }
}
    


