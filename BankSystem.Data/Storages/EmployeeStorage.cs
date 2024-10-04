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
            if (!_employees.ContainsKey(employee.Passport))
            {
                _employees.Add(employee.Passport, employee);
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
