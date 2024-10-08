using BankSystem.Domain.Models;

namespace BankSystem.App.Interfaces
{
    public interface IEmployeeStorage : IStorage<Employee>
    {
        void AddAccount(Client client, Account account);

        void UpdateAccount(Client client, Account account);

        void DeleteAccount(Client client, Account account);

        void AddAccountToEmployee(int passport, Account account);

        IEnumerable<Employee> GetAllEmployees();

        Employee GetEmployeeByPassport(int passport);

        void AddEmployee(Employee employee);

        bool EmployeeExists(int passport);
    }
}
