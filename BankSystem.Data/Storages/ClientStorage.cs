using BankSystem.Domain.Models;
using BankSystem.App.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data.Storages
{
    public class ClientStorage  : IClientStorage
    {
        private readonly BankSystemDbContext _dbContext;

        public ClientStorage(BankSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Client GetById(Guid clientId)
        {
            return _dbContext.Clients.Include(c => c.Accounts).FirstOrDefault(c => c.Id == clientId);
        }

        public void Add(Client client)
        {
            _dbContext.Clients.Add(client);
            _dbContext.SaveChanges();
        }

        public void Update(Client client)
        {
            if (client.Id == Guid.Empty)
            {
                throw new Exception("Id клиента не может быть пустым!");
            }

            _dbContext.Clients.Update(client);
            _dbContext.SaveChanges();
        }

        public void Delete(Client client)
        {
            _dbContext.Clients.Remove(client);
            _dbContext.SaveChanges();
        }

        public void AddAccount(Guid clientId, Account account)
        {
            var client = _dbContext.Clients.Include(c => c.Accounts).FirstOrDefault(c => c.Id == clientId);
            if (client == null)
            {
                throw new Exception("Клиент не найден!");
            }

            client.Accounts.Add(account);
            _dbContext.SaveChanges();
        }

        public void DeleteAccount(Guid clientId, Guid accountId)
        {
            var client = _dbContext.Clients.Include(c => c.Accounts).FirstOrDefault(c => c.Id == clientId);
            if (client == null)
            {
                throw new Exception("Клиент не найден!");
            }

            var account = client.Accounts.FirstOrDefault(a => a.Id == accountId);
            if (account != null)
            {
                client.Accounts.Remove(account);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Лицевой счет не найден!");
            }
        }

        public Client GetYoungestClient()
        {
            return _dbContext.Clients.OrderBy(c => c.Age).FirstOrDefault();
        }

        public Client GetOldestClient()
        {
            return _dbContext.Clients.OrderByDescending(c => c.Age).FirstOrDefault();
        }

        public double GetAverageAge()
        {
            if (!_dbContext.Clients.Any())
            {
                return 0;
            }

            return _dbContext.Clients.Average(c => c.Age);
        }

        public IEnumerable<Client> GetFilteredClients(Func<Client, bool> filter = null, int pageNumber = 1, int pageSize = 10)
        {
            var query = _dbContext.Clients.Include(c => c.Accounts).AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter).AsQueryable();
            }

            return query
                .OrderBy(c => c.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}