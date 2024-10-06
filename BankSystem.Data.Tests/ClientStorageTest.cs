using BankSystem.Domain.Models;
using Xunit;
using BankSystem.Data.Storages;

namespace BankSystem.Data.Tests
{
    public class ClientStorageTest
    {
        private ClientStorage InitializeStorageWithClients()
        {
            var storage = new ClientStorage();
            var clients = new List<Client>()
            {
                new Client { Name = "Иван", Surname = "Беку", Passport = 123456, Age = 21 },
                new Client { Name = "Мария", Surname = "Гунько", Passport = 124565, Age = 30 },
                new Client { Name = "Максим", Surname = "Голов", Passport = 857441, Age = 64 },
                new Client { Name = "Вера", Surname = "Улова", Passport = 893615, Age = 18 },
                new Client { Name = "Никита", Surname = "Махов", Passport = 789123, Age = 55 }
            };

            storage.AddRange(clients);

            return storage;
        }

        [Fact]
        public void AddClient_ClientDoesNotExist_ClientAddedSuccessfully()
        {
            // Arrange
            var storage = new ClientStorage();
            var clients = new List<Client>()
            {
                new Client { Name = "Иван", Surname = "Беку", Passport = 123456, Age = 21 },
                new Client { Name = "Мария", Surname = "Гунько", Passport = 124565, Age = 30 },
                new Client { Name = "Максим", Surname = "Голов", Passport = 857441, Age = 64 },
                new Client { Name = "Вера", Surname = "Улова", Passport = 893615, Age = 18 },
                new Client { Name = "Никита", Surname = "Махов", Passport = 789123, Age = 55 }
            };

            // Act
            storage.AddRange(clients);

            // Assert
            Assert.Equal(5, storage.GetAllClients().Count());
        }

        [Fact]
        public void GetYoungestClient_ReturnsCorrectClient()
        {
            // Arrange
            var storage = InitializeStorageWithClients();

            // Act
            var youngestClient = storage.GetYoungestClient();

            // Assert
            Assert.Equal(("Вера", 18), (youngestClient.Name, youngestClient.Age));
        }

        [Fact]
        public void GetOldestClient_ReturnsCorrectClient()
        {
            // Arrange
            var storage = InitializeStorageWithClients();

            // Act
            var oldestClient = storage.GetOldestClient();

            // Assert
            Assert.Equal(("Максим", 64), (oldestClient.Name, oldestClient.Age));
        }

        [Fact]
        public void GetAverageAge_ReturnsCorrectAverage()
        {
            // Arrange
            var storage = InitializeStorageWithClients();

            // Act
            var averageAge = storage.GetAverageAge();

            // Assert
            Assert.Equal(37.6, averageAge);
        }
    }
}
