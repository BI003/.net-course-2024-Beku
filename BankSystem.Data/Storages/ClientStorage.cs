using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public class ClientStorage 
    {
        private List<Client> _clients;

        public ClientStorage() 
        {
            _clients = new List<Client>();
        }

        public void AddClient(Client client)
        {
            if(!_clients.Contains(client))
            {
                _clients.Add(client);
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
            return _clients.OrderBy(c => c.Age).FirstOrDefault();
        }

        public Client GetOldestClient()
        {
            return _clients.OrderByDescending(c => c.Age).FirstOrDefault();
        }

        public double GetAverageAge()
        {
            return _clients.Average(c => c.Age);
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _clients.AsEnumerable();
        }
    }
}