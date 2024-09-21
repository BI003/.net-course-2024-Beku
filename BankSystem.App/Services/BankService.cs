using BankSystem.Domain.Models;
namespace BankSystem.App.Services
{
    public class BankService
    {
        public int Profit { get; set; }
        public int Expenses {  get; set; }
        public List<Employee> Owners { get; set; }

        public BankService(int profit, int expenses, List<Employee> owners) 
        {
            Profit = profit;
            Expenses = expenses;
            Owners = owners;
        }
        
        public int OwnerSalaryCalculation() 
        {
            int ownerCount = 0;

            foreach (var owner in Owners)
            {
                if (owner.IsOwner)
                {
                    ownerCount++;
                }
            }

            if (ownerCount == 0)
            {
                throw new InvalidOperationException("Нет владельцев!");
            }

            return (Profit - Expenses) / ownerCount;
        }
        

        public static Employee ConvertClientInEmployee(Client client, string contract) 
        {
            var employee = new Employee(client.Name, client.Surname, client.Passport, client.Number);
            
            employee.Contract = contract;
            
            return employee;
        }
    }
}
