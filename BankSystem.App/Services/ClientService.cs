using BankSystem.App.Exceptions;
using BankSystem.Domain.Models;
using BankSystem.App.Interfaces;

namespace BankSystem.App.Services
{
    public class ClientService
    {
        private readonly IClientStorage _clientStorage;

        public ClientService(IClientStorage clientStorage)
        {
            _clientStorage = clientStorage;
        }

        public void AddClient(Client client)
        {
            if (client.Passport == 0)
            {
                throw new MissingPassportException("У клиента должны быть паспортные данные!");
            }

            if (client.Age < 18)
            {
                throw new UnderageClientException("Клиент не может быть моложе 18 лет!");
            }

            _clientStorage.Add(client);
            AddDefaultAccount(client.Id);
        }

        private void AddDefaultAccount(Guid clientId)
        {
            var defaultAccount = new Account
            {
                CurrencyName = "USD",
                Amount = 0
            };

            _clientStorage.AddAccount(clientId, defaultAccount);
        }

        public Client GetClientById(Guid clientId)
        {
            return _clientStorage.GetById(clientId);
        }

        public void UpdateClient(Guid clientId, Client updatedClient)
        {
            updatedClient.Id = clientId; // Убедитесь, что Id установлен
            _clientStorage.Update(updatedClient);
        }

        public void DeleteClient(Guid clientId)
        {
            var client = _clientStorage.GetById(clientId);
            if (client == null)
            {
                throw new Exception("Клиент не найден!");
            }

            _clientStorage.Delete(client);
        }

        public void AddAdditionalAccount(Guid clientId, Account newAccount)
        {
            _clientStorage.AddAccount(clientId, newAccount);
        }

        public void DeleteAccount(Guid clientId, Guid accountId)
        {
            _clientStorage.DeleteAccount(clientId, accountId);
        }

        public IEnumerable<Client> GetFilteredClients(Func<Client, bool> filter = null, int pageNumber = 1, int pageSize = 10)
        {
            return _clientStorage.GetFilteredClients(filter, pageNumber, pageSize);
        }
    }
}
