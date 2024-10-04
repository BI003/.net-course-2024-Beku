﻿using BankSystem.App.Exceptions;
using BankSystem.App.Services;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using Xunit;

namespace BancSystem.App.Tests
{
    public class ClientServiceTests
    {
        private ClientService _clientService;
        private ClientStorage _clientStorage;

        public ClientServiceTests()
        {
            _clientStorage = InitializeStorageWithClients();
            _clientService = new ClientService(_clientStorage);
        }

        private ClientStorage InitializeStorageWithClients()
        {
            var storage = new ClientStorage();
            var clients = new List<Client>
            {
                new Client { Name = "John", Surname = "Doe", Passport = 123456, Age = 30, PhoneNumber = 373600600, DateOfBirth = new DateTime(1994, 1, 1) },
                new Client { Name = "Jane", Surname = "Doe", Passport = 654321, Age = 25, PhoneNumber = 373111222, DateOfBirth = new DateTime(1999, 1, 1) }
            };
            storage.AddRange(clients);
            return storage;
        }

        [Fact]
        public void AddClient_ValidClient_ShouldAddClientWithDefaultAccount()
        {
            // Arrange
            var client = new Client
            {
                Name = "Alice",
                Surname = "Smith",
                Age = 30,
                Passport = 789123,
                PhoneNumber = 373897654,
                DateOfBirth = new DateTime(1994, 1, 1)
            };

            // Act
            _clientService.AddClient(client);

            // Assert
            Assert.Contains(client, _clientStorage.GetAllClients());
            Assert.True(client.Accounts.ContainsKey("USD"));
            Assert.Single(client.Accounts["USD"]);
            Assert.Equal(0, client.Accounts["USD"][0].Amount);
        }

        [Fact]
        public void AddClient_MissingPassport_ShouldThrowMissingPassportException()
        {
            // Arrange
            var client = new Client
            {
                Name = "Alice",
                Surname = "Smith",
                Age = 30,
                Passport = 0, // No passport
                PhoneNumber = 373123456,
                DateOfBirth = new DateTime(1994, 1, 1)
            };

            // Act
            var exception = Assert.Throws<MissingPassportException>(() => _clientService.AddClient(client));

            // Assert
            Assert.Equal("У клиента должны быть паспортные данные!", exception.Message);
        }

        [Fact]
        public void AddClient_UnderageClient_ShouldThrowUnderageClientException()
        {
            // Arrange
            var client = new Client
            {
                Name = "Alice",
                Surname = "Smith",
                Age = 17, // Underage
                Passport = 789123,
                PhoneNumber = 373852147,
                DateOfBirth = new DateTime(2006, 1, 1)
            };

            // Act
            var exception = Assert.Throws<UnderageClientException>(() => _clientService.AddClient(client));

            // Assert
            Assert.Equal("Клиент не может быть моложе 18 лет!", exception.Message);
        }

        [Fact]
        public void AddAdditionalAccount_ClientNotFound_ShouldThrowException()
        {
            // Arrange
            var passport = 123999; // Non-existing passport
            var newAccount = new Account
            {
                Currency = new Currency { Code = "USD", Name = "Dollar" },
                Amount = 100
            };

            // Act 
            var exception = Assert.Throws<Exception>(() => _clientService.AddAdditionalAccount(passport, newAccount));

            // Assert
            Assert.Equal("Клиент не найден!", exception.Message);
        }

        [Fact]
        public void EditAccount_ValidData_ShouldEditAccount()
        {
            // Arrange
            var client = _clientStorage.GetAllClients().First(c => c.Passport == 123456);

            if (!client.Accounts.ContainsKey("USD"))
            {
                client.Accounts["USD"] = new List<Account> { new Account { Currency = new Currency { Code = "USD", Name = "Dollar" }, Amount = 0 } };
            }

            var newAmount = 200;

            // Act
            _clientService.EditAccount(client.Passport, "USD", newAmount);

            // Assert
            Assert.Equal(newAmount, client.Accounts["USD"][0].Amount);
        }

        [Fact]
        public void GetFilteredClients_WithValidName_ShouldReturnClients()
        {
            // Act
            var result = _clientService.GetFilteredClients(name: "John");

            // Assert
            Assert.Single(result);
            Assert.Equal("John", result.First().Name);
        }
    }
}
