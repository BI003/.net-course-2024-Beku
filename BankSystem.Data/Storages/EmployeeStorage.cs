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

        // Получить сотрудника по идентификатору
        public Employee GetById(Guid employeeId)
        {
            return _dbContext.Employees.Include(e => e.Accounts).FirstOrDefault(e => e.Id == employeeId);
        }

        // Добавить нового сотрудника
        public void Add(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
        }

        // Изменить сотрудника
        public void Update(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            _dbContext.SaveChanges();
        }

        // Удалить сотрудника
        public void Delete(Employee employee)
        {
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
        }

        // Добавить лицевой счет
        public void AddAccount(Guid employeeId, Account account)
        {
            var employee = GetById(employeeId);
            if (employee == null)
            {
                throw new Exception("Сотрудник не найден!");
            }

            employee.Accounts.Add(account);
            _dbContext.SaveChanges();
        }

        // Удалить лицевой счет
        public void DeleteAccount(Guid employeeId, Guid accountId)
        {
            var employee = GetById(employeeId);
            if (employee == null)
            {
                throw new Exception("Сотрудник не найден!");
            }

            var account = employee.Accounts.FirstOrDefault(a => a.Id == accountId);
            if (account != null)
            {
                employee.Accounts.Remove(account);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Лицевой счет не найден!");
            }
        }

        // Метод возвращающий список сотрудников, удовлетворяющих фильтру (+ пагинация)
        public IEnumerable<Employee> GetFilteredEmployees(Func<Employee, bool> filter = null, int pageNumber = 1, int pageSize = 10)
        {
            var query = _dbContext.Employees.Include(e => e.Accounts).AsQueryable();

            // Применяем фильтр, если он указан
            if (filter != null)
            {
                query = query.Where(filter).AsQueryable();
            }

            // Получаем отфильтрованный и отсортированный список с пагинацией
            return query
                .OrderBy(e => e.Name) // Замените на необходимый порядок сортировки
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
