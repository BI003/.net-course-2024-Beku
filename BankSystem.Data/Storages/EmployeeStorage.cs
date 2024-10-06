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
