using BankSystem.Domain.Models;
namespace BankSystem.App.Services
{
    public class BankService
    {
        public int Profit { get; set; }
        public int Expenses {  get; set; }
        public int NumberOwners { get; set; }

        public BankService(int profit, int expenses, int numberOwners) 
        {
            Profit = profit;
            Expenses = expenses;
            NumberOwners = numberOwners;
        }
        
        public static int OwnerSalaryCalculation(int profit, int expenses, int numberOwners)
        {
             

            var ownersSalary = (profit - expenses) / numberOwners;

            return ownersSalary;
        }
        
        public static Employee ConvertClientInEmployee(Client client, string contract) 
        {
            var employee = new Employee();
            employee.Name = client.Name;
            employee.Surname = client.Surname;
            employee.Passport = client.Passport;
            employee.PhoneNumber = client.PhoneNumber;
            employee.Contract = contract;
            
            return employee;
        }

        public List<Client> FindYoungerClients(List<Client> clients, int limitAge)
        {
            return clients.Where(client => client.Age < limitAge).ToList();
        }
    }
}
