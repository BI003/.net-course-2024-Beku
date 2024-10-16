using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data.Storages
{
    public class EmployeeStorage : IEmployeeStorage
    {
        private readonly BankSystemDbContext _dbContext;

        public EmployeeStorage(BankSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Employee GetById(Guid employeeId)
        {
            return _dbContext.Employees.Include(e => e.Accounts).FirstOrDefault(e => e.Id == employeeId);
        }

        public void Add(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
        }

        public void Update(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            _dbContext.SaveChanges();
        }

        public void Delete(Employee employee)
        {
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Employee> GetFilteredEmployees(Func<Employee, bool> filter = null, int pageNumber = 1, int pageSize = 10)
        {
            var query = _dbContext.Employees.Include(e => e.Accounts).AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter).AsQueryable();
            }

            return query
                .OrderBy(e => e.Name) 
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
