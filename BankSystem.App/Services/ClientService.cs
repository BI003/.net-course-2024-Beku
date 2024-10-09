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
            AddDefaultAccount(client.Passport);
        }

        private void AddDefaultAccount(int passport)
        {
            var defaultCurrency = new Currency()
            {
                Code = "USD",
                Name = "Dollar"
            };

            var defaultAccount = new Account()
            {
                Currency = defaultCurrency,
                Amount = 0
            };

            var client = _clientStorage.GetClientByPassport(passport);
            if (client != null)
            {
                _clientStorage.AddAccount(client, defaultAccount);
            }
            else
            {
                throw new Exception("Клиент не найден!");
            }
        }

        public void EditAccount(int passport, string currencyCode, decimal newAmount)
        {
            var client = _clientStorage.GetClientByPassport(passport);

            if (client == null)
            {
                throw new Exception("Клиент не найден!");
            }

            if (!client.Accounts.TryGetValue(currencyCode, out var accountList) || accountList == null || !accountList.Any())
            {
                throw new Exception($"Счета в валюте {currencyCode} не найдены.");
            }

            var account = accountList.FirstOrDefault();

            if (account == null)
            {
                throw new Exception($"Счёт в валюте {currencyCode} не найден.");
            }

            account.Amount = newAmount;
        }

        public void AddAdditionalAccount(int passport, Account newAccount)
        {
            var client = _clientStorage.GetClientByPassport(passport);

            if (client == null)
            {
                throw new Exception("Клиент не найден!");
            }

            if (!client.Accounts.ContainsKey(newAccount.Currency.Code))
            {
                client.Accounts[newAccount.Currency.Code] = new List<Account>();
            }

            client.Accounts[newAccount.Currency.Code].Add(newAccount);
        }

        public IEnumerable<Client> GetFilteredClients(Func<Client, bool> filter = null)
        {
            return _clientStorage.Get(filter);
        }
    }
}
