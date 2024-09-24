using BankSystem.Domain.Models;

namespace BankSystem.App.Services
{
    public class TestDataGenerator
    {
        private static Random _random = new Random();
        public List<Client> GenerateClient()
        {
            var clients = new List<Client>(1000);

            for (int i = 0; i < 1000; i++)
            {
                var client = new Client();

                client.Name = GenerateRandomName();
                client.Surname = GenerateRandomSurname();
                client.Passport = GenerateRandomPassport();
                client.Number = GenerateRandomNumber();
                
                clients.Add(client);
            }
            return clients;
        }
        public Dictionary<int, Client> GenerateClientsDictionary() 
        {
            var clientDictionary = new Dictionary<int, Client>();
            for (int i = 0;i < 1000; i++)
            {
                var client = new Client();

                client.Name = GenerateRandomName();
                client.Surname= GenerateRandomSurname();
                client.Passport = GenerateRandomPassport();
                client.Number = GenerateRandomNumber();

                clientDictionary.TryAdd(client.Number, client);
            }
            return clientDictionary;
        }


        public List<Employee> GenerateEmployee()
        {
            var employees = new List<Employee>(1000);

            for (int i = 0; i < 1000; i++)
            {
                var employee = new Employee();

                employee.Name = GenerateRandomName();
                employee.Surname = GenerateRandomSurname();
                employee.Passport = GenerateRandomPassport();
                employee.Number = GenerateRandomNumber();
                employee.Contract = "Контракт подписан";

                employees.Add(employee);
            }
            return employees;
        }

        public string GenerateRandomName()
        {
            string[] name = { "Иван", "Тимофей", "Владимир", "Мария", "Наталья" };
            return name[_random.Next(name.Length)]; 
        }
        public string GenerateRandomSurname()
        {
            string[] surname = { "Иванов", "Севцов", "Фёдоров", "Сырбу", "Грачёв" };
            return surname[_random.Next(surname.Length)];
        }
        public int GenerateRandomPassport() 
        {
            return _random.Next(1000, 2000);
        }
        public int GenerateRandomNumber()
        {
            return _random.Next(3731000, 3732000);
        }
    }  
}
