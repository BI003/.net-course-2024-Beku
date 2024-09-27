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
            bool clientExists = clientAndAccount.ContainsKey(existingClient);

            // Assert
            Assert.Equal(clientExists, true);
        }

        [Fact]
        public void GenerateEmployees_EmployeeAlreadyExists_True()
        {
            // Arrange
            var generator = new TestDataGenerator();
            var employees = generator.GenerateEmployees();

            var existingEmployee = new Employee()
            {
                Name = employees.First().Name,
                Surname = employees.First().Surname,
                Contract = employees.First().Contract,
            };

            // Act
            bool employeeExists = employees.Contains(existingEmployee);

            // Assert
            Assert.Equal(employeeExists, true);
        }
    }
}
