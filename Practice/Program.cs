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