using BankSystem.App.Services;
using BankSystem.Domain.Models;

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

        var client = new Client();
        client.Name = "Дмитрий";
        client.Surname = "Дмитриевич";
        client.Passport = 987654;
        client.Number = 373898989;
        Console.WriteLine($"Клиент банка: {client.Name}, {client.Surname}, {client.Passport}, {client.Number}");
        var newContract2 = " Клиент стал Сотрудником";
        var newEmployee = BankService.ConvertClientInEmployee(client, newContract2);
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
    }
}