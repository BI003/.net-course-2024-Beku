using BankSystem.App.Exceptions;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;

namespace BankSystem.App.Services
{
    public class EmployeeService
    {
        private readonly EmployeeStorage _employeeStorage;

        public EmployeeService(EmployeeStorage employeeStorage)
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

            if (_employeeStorage.EmployeeExists(employee.Passport))
            {
                throw new Exception("Сотрудник с таким паспортом уже существует.");
            }

            _employeeStorage.AddEmployee(employee);
            AddDefaultAccount(employee.Passport);
        }

        private void AddDefaultAccount(int passport)
        {
            var defaultCurrency = new Currency()
            {
                Code = "USD",
                Name = "Dollar"
            };

            var defaultAccount = new Account()
            {
                Currency = defaultCurrency,
                Amount = 0
            };

            _employeeStorage.AddAccountToEmployee(passport, defaultAccount);
        }

        public void EditSalary(int passport, int newSalary)
        {
            var employee = _employeeStorage.GetEmployeeByPassport(passport);

            if (employee == null)
            {
                throw new Exception("Сотрудник не найден!");
            }

            employee.Salary = newSalary;
        }

        public void EditAccount(int passport, string currencyCode, decimal newAmount)
        {
            var employee = _employeeStorage.GetEmployeeByPassport(passport);

            if (employee == null)
            {
                throw new Exception("Сотрудник не найден!");
            }

            if (!employee.Accounts.TryGetValue(currencyCode, out var accountList) || accountList == null || !accountList.Any())
            {
                throw new Exception($"Счета в валюте {currencyCode} не найдены.");
            }

            var account = accountList.FirstOrDefault();

            if (account == null)
            {
                throw new Exception($"Счёт в валюте {currencyCode} не найден.");
            }

            account.Amount = newAmount;
        }

        public void AddAdditionalAccount(int passport, Account newAccount)
        {
            var employee = _employeeStorage.GetEmployeeByPassport(passport);

            if (employee == null)
            {
                throw new Exception("Сотрудник не найден!");
            }

            if (!employee.Accounts.ContainsKey(newAccount.Currency.Code))
            {
                employee.Accounts[newAccount.Currency.Code] = new List<Account>();
            }

            employee.Accounts[newAccount.Currency.Code].Add(newAccount);
        }

        public IEnumerable<Employee> GetFilteredEmployees(Func<Employee, bool> filter = null)
        {
            var employees = _employeeStorage.GetAllEmployees();

            if (filter != null)
            {
                employees = employees.Where(filter);
            }
            return employees;
        }
    }
}
