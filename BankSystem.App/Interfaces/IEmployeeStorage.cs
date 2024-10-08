using BankSystem.Domain.Models;

namespace BankSystem.App.Interfaces
{
    public interface IEmployeeStorage : IStorage<Employee>
    {
        void AddAccount(int passport, Account account);

        void UpdateAccount(Employee employee, Account account);

        void DeleteAccount(Employee employee, Account account);

        IEnumerable<Employee> GetAllEmployees();

        Employee GetEmployeeByPassport(int passport);

        bool EmployeeExists(int passport);
    }
}
