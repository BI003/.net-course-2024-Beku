using BankSystem.Domain.Models;
namespace BankSystem.App.Services
{
    public class BankService
    {
        public int NumberOwners { get; set; }
        public int Profit { get; set; }
        public int Expenses {  get; set; }

        public BankService(int numberOwners, int profit, int expenses) 
        {
            NumberOwners = numberOwners;
            Profit = profit;
            Expenses = expenses;
        }
        
        public int OwnerSalaryCalculation() 
        {
            var OwnersSalary = (Profit - Expenses) / NumberOwners;
            return OwnersSalary;
        }
        

        public static Employee ConvertClientInEmployee(Client client, string contract) 
        {
            var employee = new Employee(client.Name, client.Surname, client.Passport, client.Number);
            
            employee.Contract = contract;
            
            return employee;
        }
    }
}
