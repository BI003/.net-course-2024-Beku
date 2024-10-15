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

        // Получить клиента по идентификатору
        public Client GetById(Guid clientId)
        {
            return _dbContext.Clients.Include(c => c.Accounts).FirstOrDefault(c => c.Id == clientId);
        }

        // Добавить нового клиента
        public void Add(Client client)
        {
            _dbContext.Clients.Add(client);
            _dbContext.SaveChanges();
        }

        // Изменить клиента
        public void Update(Client client)
        {
            _dbContext.Clients.Update(client);
            _dbContext.SaveChanges();
        }

        // Удалить клиента
        public void Delete(Client client)
        {
            _dbContext.Clients.Remove(client);
            _dbContext.SaveChanges();
        }

        // Добавить лицевой счет
        public void AddAccount(Guid clientId, Account account)
        {
            var client = GetById(clientId);
            if (client == null)
            {
                throw new Exception("Клиент не найден!");
            }

            client.Accounts.Add(account);
            _dbContext.SaveChanges();
        }

        // Удалить лицевой счет
        public void DeleteAccount(Guid clientId, Guid accountId)
        {
            var client = GetById(clientId);
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

        // Метод возвращающий список клиентов, удовлетворяющих фильтру (+ пагинация)
        public IEnumerable<Client> GetFilteredClients(Func<Client, bool> filter = null, int pageNumber = 1, int pageSize = 10)
        {
            var query = _dbContext.Clients.Include(c => c.Accounts).AsQueryable();

            // Применяем фильтр, если он указан
            if (filter != null)
            {
                query = query.Where(filter).AsQueryable();
            }

            // Получаем отфильтрованный и отсортированный список с пагинацией
            return query
                .OrderBy(c => c.Name) // Замените на необходимый порядок сортировки
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}