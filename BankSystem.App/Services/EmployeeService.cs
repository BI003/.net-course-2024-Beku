using BankSystem.App.Exceptions;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.App.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeStorage _employeeStorage;

        public EmployeeService(IEmployeeStorage employeeStorage)
        {
            _employeeStorage = employeeStorage;
        }

        public void AddEmployee(Employee employee)
        {
            if (employee.Passport == 0)
            {
                throw new MissingPassportException("У сотрудника должны быть паспортные данные!");
            }

            if (employee.Age < 18)
            {
                throw new UnderageClientException("Сотрудник не может быть моложе 18 лет!");
            }

            if (_employeeStorage.GetById(employee.Id) != null)
            {
                throw new Exception("Сотрудник с таким паспортом уже существует.");
            }

            _employeeStorage.Add(employee);
            AddDefaultAccount(employee);
        }

        private void AddDefaultAccount(Employee employee)
        {
            var defaultAccount = new Account
            {
                CurrencyName = "USD",
                Amount = 0
            };

            _employeeStorage.AddAccount(employee.Id, defaultAccount);
        }

        public void EditSalary(Guid employeeId, int newSalary)
        {
            var employee = _employeeStorage.GetById(employeeId);

            if (employee == null)
            {
                throw new Exception("Сотрудник не найден!");
            }

            employee.Salary = newSalary;
            _employeeStorage.Update(employee);
        }

        public void EditAccount(Guid employeeId, Guid accountId, decimal newAmount)
        {
            var employee = _employeeStorage.GetById(employeeId);

            if (employee == null)
            {
                throw new Exception("Сотрудник не найден!");
            }

            var account = employee.Accounts.FirstOrDefault(a => a.Id == accountId);
            if (account == null)
            {
                throw new Exception("Счёт не найден!");
            }

            account.Amount = newAmount;
            _employeeStorage.Update(employee); // Обновляем сотрудника
        }

        public void AddAdditionalAccount(Guid employeeId, Account newAccount)
        {
            _employeeStorage.AddAccount(employeeId, newAccount);
        }

        public void DeleteAccount(Guid employeeId, Guid accountId)
        {
            _employeeStorage.DeleteAccount(employeeId, accountId);
        }

        public IEnumerable<Employee> GetFilteredEmployees(Func<Employee, bool> filter = null, int pageNumber = 1, int pageSize = 10)
        {
            return _employeeStorage.GetFilteredEmployees(filter, pageNumber, pageSize);
        }
    }
}
