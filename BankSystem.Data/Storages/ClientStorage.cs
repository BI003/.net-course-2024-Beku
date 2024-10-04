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
            if (!_clients.ContainsKey(client.Passport))
            {
                _clients.Add(client.Passport, client);
            }   
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
            return _clients.Values.OrderBy(c => c.Age).FirstOrDefault();
        }

        public Client GetOldestClient()
        {
            return _clients.Values.OrderByDescending(c => c.Age).FirstOrDefault();
        }

        public double GetAverageAge()
        {
            return _clients.Values.Average(c => c.Age);
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _clients.Values.AsEnumerable();
        }
    }
}