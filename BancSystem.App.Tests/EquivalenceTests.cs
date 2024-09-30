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

            var firstClient = clientAndAccount.Keys.First();

            var existingClient = new Client()
            {
                Name = firstClient.Name,
                Surname = firstClient.Surname,
                Age = firstClient.Age,
                PhoneNumber = firstClient.PhoneNumber,
                Passport = firstClient.Passport,
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

            var firstEmployee = employees.First();

            var existingEmployee = new Employee()
            {
                Name = firstEmployee.Name,
                Surname = firstEmployee.Surname,
                Contract = firstEmployee.Contract,
                Salary = firstEmployee.Salary,
                Passport = firstEmployee.Passport,
            };

            // Act
            bool employeeExists = employees.Contains(existingEmployee);

            // Assert
            Assert.Equal(employeeExists, true);
        }
    }
}
