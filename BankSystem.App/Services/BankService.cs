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
            var employee = new Employee(client.Name, client.Surname, client.Passport, client.Number);
            
            employee.Contract = contract;
            
            return employee;
        }
    }
}
