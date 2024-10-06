using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public class ClientStorage 
    {
        private Dictionary<int, Client> _clients;

        public ClientStorage() 
        {
            _clients = new Dictionary<int, Client>();
        }

        public void AddClient(Client client)
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

        public bool ClientExists(int passport)
        {
            return _clients.ContainsKey(passport);
        }

        public void AddAccountToClient(int passport, Account account)
        {
            if (_clients.TryGetValue(passport, out var client))
            {
                if (!client.Accounts.ContainsKey(account.Currency.Code))
                {
                    client.Accounts[account.Currency.Code] = new List<Account>();
                }
                client.Accounts[account.Currency.Code].Add(account);
            }
            else
            {
                throw new Exception("Клиент не найден!");
            }
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
                AddClient(client);
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
            return _clients.Values.AsEnumerable();
        }
    }
}