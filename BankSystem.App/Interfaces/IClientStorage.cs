using BankSystem.Domain.Models;

namespace BankSystem.App.Interfaces
{
    public interface IClientStorage : IStorage<Client>
    {
        Client GetById(Guid clientId);
        void Add(Client client);
        void Update(Client client);
        void Delete(Client client);
        void AddAccount(Guid clientId, Account account);
        void DeleteAccount(Guid clientId, Guid accountId);
        IEnumerable<Client> GetFilteredClients(Func<Client, bool> filter = null, int pageNumber = 1, int pageSize = 10);
    }
}

