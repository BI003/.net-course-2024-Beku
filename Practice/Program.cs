using BankSystem.App.Services;
using BankSystem.Domain.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var employee = new Employee("Иван", "Беку", 123, 37360125);

        Console.WriteLine($"До: {employee.Contract}");
        var newContract = "Контракт создан!";
        CreateContract(employee, newContract);
        Console.WriteLine($"После: {employee.Contract}");

        var currency = new Currency("Доллары", 10);

        Console.WriteLine($"До: {currency.Price}");
        var newPrice = 20;
        Update(ref currency ,newPrice);
        Console.WriteLine($"После: {currency.Price}");



        var employee1 = new Employee("Иван", "Беку", 123, 37360125, true);
        var employee2 = new Employee("Пётр", "Петров", 456, 37360126, false);
        var employee3 = new Employee("Анна", "Смирнова", 789, 37360127, true);

        var owners = new List<Employee> { employee1, employee2, employee3 };
        var bankService = new BankService(10000, 2000 ,owners);

        var ownerSalary = bankService.OwnerSalaryCalculation();
        Console.WriteLine($"Зарплата каждого владельца: {ownerSalary}");



        var client = new Client("Дмитрий", "Дмитриевич", 987654, 373898989);
        
        Console.WriteLine($"Клиент банка: {client.Name}, {client.Surname}, {client.Passport}, {client.Number}");
        var newContract2 = " Клиент стал Сотрудником";
        var newEmployee = BankService.ConvertClientInEmployee(client, newContract2);
        Console.WriteLine($"Сотрудник: {newEmployee.Name}, {newEmployee.Surname}, Контракт: {newEmployee.Contract }");

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