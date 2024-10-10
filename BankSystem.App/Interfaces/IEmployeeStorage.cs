using BankSystem.Domain.Models;

namespace BankSystem.App.Interfaces
{
    public interface IEmployeeStorage : IStorage<Employee>
    {
        void AddAccount(Employee employee, Account account);

        void UpdateAccount(Employee employee, Account account);

        void DeleteAccount(Employee employee, Account account);

        IEnumerable<Employee> GetAllEmployees();

        bool EmployeeExists(int passport);
    }
}
