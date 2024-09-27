using BankSystem.App.Services;
using BankSystem.Domain.Models;
using Xunit;

namespace BancSystem.App.Tests
{
    public class EquivalenceTests
    {
        [Fact]
        public void GetHashCodeNecessityPositivTest()
        {
            // Arrange
            var generator = new TestDataGenerator();
            var clientAndAccount = generator.GenerateDictionaryClientsAndAccounts();

            var existingClient = new Client()
            {
                Name = clientAndAccount.Keys.First().Name,
                Surname = clientAndAccount.Keys.First().Surname,
            };

            // Act
            bool existingAccount = clientAndAccount.ContainsKey(existingClient);

            // Assert
            Assert.Equal(existingAccount, true);
        }
    }
}
