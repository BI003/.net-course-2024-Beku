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
        employee.PhoneNumber = 37360125;
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
        newclient.PhoneNumber = 373898989;
        Console.WriteLine($"Клиент банка: {newclient.Name}, {newclient.Surname}, {newclient.Passport}, {newclient.PhoneNumber}");
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
        var clientDictionary = generator.GenerateClientsDictionaryFromList(clients);
        var employees = generator.GenerateEmployees();

        var searchByNumber = 3731111;
        var iterations = 1000;

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            var foundClient = clients.FirstOrDefault(client => client.PhoneNumber == searchByNumber);
        }
        stopwatch.Stop();
        long listSearchTime = stopwatch.ElapsedMilliseconds; 
        Console.WriteLine($"Время поиска в списке: {listSearchTime} миллисекунд");

        stopwatch.Reset();
        stopwatch.Start(); 
        for (int i = 0; i < iterations; i++)
        {
            var clientExists = clientDictionary.TryGetValue(searchByNumber, out var foundClient2);
        }
        stopwatch.Stop(); 
        long dictionarySearchTime = stopwatch.ElapsedMilliseconds; 
        Console.WriteLine($"Время поиска в словаре: {dictionarySearchTime} миллисекунд");

        var limitAge = 19;
        var youngClients = bankService.FindYoungerClients(clients, limitAge);
        Console.WriteLine($"Клиенты младше {limitAge} лет: {youngClients.Count} клиентов");

        var employeeWithMinSalary = generator.FindEmployeeWithMinSalary(employees);
        if (employeeWithMinSalary != null)
        {
            Console.WriteLine($"Сотрудник с минимальной зарплатой: {employeeWithMinSalary.Name} {employeeWithMinSalary.Surname}, Зарплата: {employeeWithMinSalary.Salary}");
        }
        else
        {
            Console.WriteLine("Сотрудники не найдены.");
        }

        stopwatch.Reset(); 
        stopwatch.Start(); 
        for (int i = 0; i < iterations; i++)
        {
            var lastClientInDictionary = clientDictionary.Values.LastOrDefault();
        }
        stopwatch.Stop(); 
        long lastOrDefaultSearchTime = stopwatch.ElapsedMilliseconds; 
        Console.WriteLine($"Время поиска последнего клиента через LastOrDefault: {lastOrDefaultSearchTime} миллисекунд");

        stopwatch.Reset(); 
        stopwatch.Start(); 
        for (int i = 0; i < iterations; i++)
        {
            var lastClientKey = clientDictionary.Keys.LastOrDefault();
            var foundLastClientByKey = clientDictionary[lastClientKey];
        }
        stopwatch.Stop(); 
        long keySearchTime = stopwatch.ElapsedMilliseconds; 
        Console.WriteLine($"Время поиска последнего клиента по ключу: {keySearchTime} миллисекунд");
    }
}



