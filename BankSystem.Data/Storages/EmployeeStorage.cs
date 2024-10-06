using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public class EmployeeStorage
    {
        private Dictionary<int, Employee> _employees;

        public EmployeeStorage()
        {
            _employees = new Dictionary<int, Employee>();
        }

        public void AddEmployee(Employee employee)
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

        public bool EmployeeExists(int passport)
        {
            return _employees.ContainsKey(passport);
        }

        public Employee GetEmployeeByPassport(int passport)
        {
            if (_employees.TryGetValue(passport, out var employee))
            {
                return employee;
            }
            throw new Exception("Сотрудник не найден!");
        }

        public void AddAccountToEmployee(int passport, Account account)
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

        public void AddRange(IEnumerable<Employee> employees)
        {
            foreach (var employee in employees)
            {
                AddEmployee(employee);
            }
        }

        public Employee GetYoungestEmployee()
        {
            return _employees.Values.OrderBy(e => e.Age).FirstOrDefault();
        }

        public Employee GetOldestEmployee()
        {
            return _employees.Values.OrderByDescending(e => e.Age).FirstOrDefault();
        }

        public double GetAverageAge()
        {
            return _employees.Values.Average(e => e.Age);
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employees.Values.AsEnumerable();
        }
    }
}
