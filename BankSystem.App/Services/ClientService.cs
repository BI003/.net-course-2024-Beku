﻿using BankSystem.App.Exceptions;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;

namespace BankSystem.App.Services
{
    public class ClientService
    {
        private readonly ClientStorage _clientStorage;

        public ClientService(ClientStorage clientStorage)
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
            _clientStorage.AddClient(client);

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

            if (!client.Accounts.ContainsKey(defaultCurrency.Code))
            {
                client.Accounts[defaultCurrency.Code] = new List<Account>(); 
            }
            client.Accounts[defaultCurrency.Code].Add(defaultAccount);
        }

        public void AddAdditionalAccount(int passport, Account newAccount)
        {
            var client = _clientStorage.GetAllClients().FirstOrDefault(c => c.Passport == passport);

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

        public void EditAccount(int passport, string currencyCode, decimal newAmount)
        {
            var client = _clientStorage.GetAllClients().FirstOrDefault(c => c.Passport == passport);

            if (client == null)
            {
                throw new Exception("Клиент не найден!");
            }

            if (!client.Accounts.TryGetValue(currencyCode, out var accountList))
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

        public IEnumerable<Client> GetFilteredClients(string name = null, string surname = null, int? passport = null, string phoneNumber = null, DateTime? dateOfBirthFrom = null, DateTime? dateOfBirthTo = null)
        {
            var clients = _clientStorage.GetAllClients();

            if (!string.IsNullOrWhiteSpace(name))
            {
                clients = clients.Where(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(surname))
            {
                clients = clients.Where(c => c.Surname.Equals(surname, StringComparison.OrdinalIgnoreCase));
            }

            if (passport.HasValue)
            {
                clients = clients.Where(c => c.Passport == passport.Value);
            }

            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                clients = clients.Where(c => c.PhoneNumber == phoneNumber);
            }

            if (dateOfBirthFrom.HasValue)
            {
                clients = clients.Where(c => c.DateOfBirth >= dateOfBirthFrom.Value);
            }

            if (dateOfBirthTo.HasValue)
            {
                clients = clients.Where(c => c.DateOfBirth <= dateOfBirthTo.Value);
            }

            return clients;
        }
    }
}
