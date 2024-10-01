using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public class EmployeeStorage
    {
        private List<Employee> _employees;

        public EmployeeStorage()
        {
            _employees = new List<Employee>();
        }

        public void AddEmployee(Employee employee)
        {
            if (!_employees.Contains(employee))
            {
                _employees.Add(employee);
            }
        }

        public Employee GetYoungestEmployee()
        {
            return _employees.OrderBy(e => e.Age).FirstOrDefault();
        }

        public Employee GetOldestEmployee()
        {
            return _employees.OrderByDescending(e => e.Age).FirstOrDefault();
        }

        public double GetAverageAge()
        {
            return _employees.Average(e => e.Age);
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employees.AsEnumerable();
        }
    }
}
