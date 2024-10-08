using BankSystem.Domain.Models;
using BankSystem.App.Interfaces;

namespace BankSystem.Data.Storages
{
    public class ClientStorage  : IClientStorage
    {
        private Dictionary<int, Client> _clients;

        public ClientStorage() 
        {
            _clients = new Dictionary<int, Client>();
        }
        private bool ClientExists(int passport)
        {
            return _clients.ContainsKey(passport);
        }

        public List<Client> Get(Func<Client, bool> filter)
        {
            var result = new List<Client>();
            foreach (var client in _clients.Values)
            {
                if (filter(client))
                {
                    result.Add(client);
                }
            }
            return result;
        }

        public void Add(Client client)
        {
            if (!ClientExists(client.Passport))
            {
                _clients.Add(client.Passport, client);
            }
            else
            {
                throw new Exception("Клиент с таким паспортом уже существует.");
            }
        }

        public void Update(Client client)
        {
            if (ClientExists(client.Passport))
            {
                _clients[client.Passport] = client;
            }
            else
            {
                throw new Exception("Клиент не найден!");
            }
        }

        public void Delete(Client client)
        {
            if (ClientExists(client.Passport))
            {
                _clients.Remove(client.Passport);
            }
            else
            {
                throw new Exception("Клиент не найден!");
            }
        }

        public void AddAccount(Client client, Account account)
        {
            if (!_clients.ContainsKey(client.Passport))
            {
                throw new Exception("Клиент не найден!");
            }

            if (!client.Accounts.ContainsKey(account.Currency.Code))
            {
                client.Accounts[account.Currency.Code] = new List<Account>();
            }

            client.Accounts[account.Currency.Code].Add(account);
        }

        public void UpdateAccount(Client client, Account account)
        {
            if (_clients.TryGetValue(client.Passport, out var existingClient))
            {
                if (existingClient.Accounts.ContainsKey(account.Currency.Code))
                {
                    var accounts = existingClient.Accounts[account.Currency.Code];
                    var existingAccount = accounts.FirstOrDefault(a => a.Currency.Code == account.Currency.Code);

                    if (existingAccount != null)
                    {
                        existingAccount.Amount = account.Amount;
                    }
                }
            }
        }

        public void DeleteAccount(Client client, Account account)
        {
            if (_clients.TryGetValue(client.Passport, out var existingClient))
            {
                if (existingClient.Accounts.ContainsKey(account.Currency.Code))
                {
                    existingClient.Accounts[account.Currency.Code].Remove(account);
                }
            }
        }
        
        public void AddClient(Client client)
        {
            Add(client);
        }

        public void AddAccountToClient(int passport, Account account)
        {
            AddAccount(GetClientByPassport(passport), account);
        }

        public Client GetClientByPassport(int passport)
        {
            if (_clients.TryGetValue(passport, out var client))
            {
                return client;
            }
            throw new Exception("Клиент не найден!");
        }

        public void AddRange(IEnumerable<Client> clients)
        {
            foreach (var client in clients)
            {
                Add(client);
            }
        }

        public Client GetYoungestClient()
        {
            Client youngestClient = null;

            foreach (var client in _clients.Values)
            {
                if (youngestClient == null || client.Age < youngestClient.Age)
                {
                    youngestClient = client;
                }
            }
            return youngestClient;
        }

        public Client GetOldestClient()
        {
            Client oldestClient = null;

            foreach (var client in _clients.Values)
            {
                if (oldestClient == null || client.Age > oldestClient.Age)
                {
                    oldestClient = client;
                }
            }
            return oldestClient;
        }

        public double GetAverageAge()
        {
            if (_clients.Count == 0)
            {
                return 0;
            }

            double totalAge = 0;
            foreach (var client in _clients.Values)
            {
                totalAge += client.Age;
            }
            return totalAge / _clients.Count;
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _clients.Values.ToList();
        }
    }
}