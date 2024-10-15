using BankSystem.Domain.Models;

namespace BankSystem.App.Interfaces
{
    public interface IEmployeeStorage : IStorage<Employee>
    {
        Employee GetById(Guid employeeId);
        void Add(Employee employee);
        void Update(Employee employee);
        void Delete(Employee employee);
        void AddAccount(Guid employeeId, Account account);
        void DeleteAccount(Guid employeeId, Guid accountId);
        IEnumerable<Employee> GetFilteredEmployees(Func<Employee, bool> filter = null, int pageNumber = 1, int pageSize = 10);
    }
}
