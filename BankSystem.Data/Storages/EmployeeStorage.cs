using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public class EmployeeStorage : IEmployeeStorage
    {
        private Dictionary<int, Employee> _employees;

        public EmployeeStorage()
        {
            _employees = new Dictionary<int, Employee>();
        }

        public bool EmployeeExists(int passport)
        {
            return _employees.ContainsKey(passport);
        }

        public List<Employee> Get(Func<Employee, bool> filter)
        {
            var result = new List<Employee>();
            foreach (var employee in _employees.Values)
            {
                if (filter(employee))
                {
                    result.Add(employee);
                }
            }
            return result;
        }

        public void Add(Employee employee)
        {
            if (!EmployeeExists(employee.Passport))
            {
                _employees.Add(employee.Passport, employee);
            }
            else
            {
                throw new Exception("Сотрудник с таким паспортом уже существует.");
            }
        }

        public void Update(Employee employee)
        {
            if (EmployeeExists(employee.Passport))
            {
                _employees[employee.Passport] = employee;
            }
            else
            {
                throw new Exception("Сотрудник не найден!");
            }
        }

        public void Delete(Employee employee)
        {
            if (EmployeeExists(employee.Passport))
            {
                _employees.Remove(employee.Passport);
            }
            else
            {
                throw new Exception("Сотрудник не найден!");
            }
        }

        public void AddAccount(int passport, Account account)
        {
            if (_employees.TryGetValue(passport, out var employee))
            {
                if (!employee.Accounts.ContainsKey(account.Currency.Code))
                {
                    employee.Accounts[account.Currency.Code] = new List<Account>();
                }
                employee.Accounts[account.Currency.Code].Add(account);
            }
            else
            {
                throw new Exception("Сотрудник не найден!");
            }
        }

        public void UpdateAccount(Employee employee, Account account)
        {
            if (_employees.TryGetValue(employee.Passport, out var existingEmployee))
            {
                if (existingEmployee.Accounts.ContainsKey(account.Currency.Code))
                {
                    var accounts = existingEmployee.Accounts[account.Currency.Code];
                    var existingAccount = accounts.FirstOrDefault(a => a.Currency.Code == account.Currency.Code);

                    if (existingAccount != null)
                    {
                        existingAccount.Amount = account.Amount;
                    }
                }
            }
        }

        public void DeleteAccount(Employee employee, Account account)
        {
            if (_employees.TryGetValue(employee.Passport, out var existingEmployee))
            {
                if (existingEmployee.Accounts.ContainsKey(account.Currency.Code))
                {
                    existingEmployee.Accounts[account.Currency.Code].Remove(account);
                }
            }
        }

        public Employee GetEmployeeByPassport(int passport)
        {
            if (_employees.TryGetValue(passport, out var employee))
            {
                return employee;
            }
            throw new Exception("Сотрудник не найден!");
        }

        

        public void AddRange(IEnumerable<Employee> employees)
        {
            foreach (var employee in employees)
            {
                Add(employee);
            }
        }

        public Employee GetYoungestEmployee()
        {
            Employee youngestEmployee = null;

            foreach (var employee in _employees.Values)
            {
                if (youngestEmployee == null || employee.Age < youngestEmployee.Age)
                {
                    youngestEmployee = employee;
                }
            }
            return youngestEmployee;
        }

        public Employee GetOldestEmployee()
        {
            Employee oldestEmployee = null;

            foreach (var employee in _employees.Values)
            {
                if (oldestEmployee == null || employee.Age > oldestEmployee.Age)
                {
                    oldestEmployee = employee;
                }
            }
            return oldestEmployee;
        }

        public double GetAverageAge()
        {
            if (_employees.Count == 0)
            {
                return 0;
            }

            double totalAge = 0;
            foreach (var employee in _employees.Values)
            {
                totalAge += employee.Age;
            }
            return totalAge / _employees.Count;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employees.Values.AsEnumerable();
        }
    }
}
