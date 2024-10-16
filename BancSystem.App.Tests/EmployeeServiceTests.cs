using BankSystem.App.Exceptions;
using BankSystem.App.Services;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using BankSystem.App.Interfaces;
using Xunit;

namespace BancSystem.App.Tests
{
  /*  public class EmployeeServiceTests
    {
        private EmployeeService _employeeService;
        private IEmployeeStorage _employeeStorage;

        public EmployeeServiceTests()
        {
            _employeeStorage = InitializeStorageWithEmployees();
            _employeeService = new EmployeeService(_employeeStorage);
        }

        private IEmployeeStorage InitializeStorageWithEmployees()
        {
            var storage = new EmployeeStorage();
            var employees = new List<Employee>
            {
                new Employee { Name = "Alice", Surname = "Johnson", Passport = 123456, Age = 30, Salary = 50000 },
                new Employee { Name = "Bob", Surname = "Smith", Passport = 654321, Age = 25, Salary = 45000 }
            };
            storage.AddRange(employees);
            return storage;
        }

        [Fact]
        public void AddEmployee_ValidEmployee_ShouldAddEmployeeWithDefaultAccount()
        {
            // Arrange
            var employee = new Employee
            {
                Name = "Charlie",
                Surname = "Brown",
                Age = 35,
                Passport = 789123,
                Salary = 60000
            };

            // Act
            _employeeService.AddEmployee(employee);

            // Assert
            Assert.Contains(employee, _employeeStorage.Get(e => true));
            Assert.True(employee.Accounts.ContainsKey("USD"));
            Assert.Single(employee.Accounts["USD"]);
            Assert.Equal(0, employee.Accounts["USD"][0].Amount);
        }

        [Fact]
        public void AddEmployee_MissingPassport_ShouldThrowMissingPassportException()
        {
            // Arrange
            var employee = new Employee
            {
                Name = "Charlie",
                Surname = "Brown",
                Age = 30,
                Passport = 0,
                Salary = 60000
            };

            // Act
            var exception = Assert.Throws<MissingPassportException>(() => _employeeService.AddEmployee(employee));

            // Assert
            Assert.Equal("У сотрудника должны быть паспортные данные!", exception.Message);
        }

        [Fact]
        public void AddEmployee_UnderageEmployee_ShouldThrowUnderageClientException()
        {
            // Arrange
            var employee = new Employee
            {
                Name = "Charlie",
                Surname = "Brown",
                Age = 17,
                Passport = 789123,
                Salary = 60000
            };

            // Act
            var exception = Assert.Throws<UnderageClientException>(() => _employeeService.AddEmployee(employee));

            // Assert
            Assert.Equal("Сотрудник не может быть моложе 18 лет!", exception.Message);
        }

        [Fact]
        public void EditSalary_ValidData_ShouldEditSalary()
        {
            // Arrange
            var passport = 123456;
            var newSalary = 70000;

            // Act
            _employeeService.EditSalary(passport, newSalary);

            // Assert
            var employee = _employeeStorage.Get(e => e.Passport == passport).First();
            Assert.Equal(newSalary, employee.Salary);
        }

        [Fact]
        public void EditAccount_ValidData_ShouldEditAccount()
        {
            // Arrange
            var employee = _employeeStorage.Get(e => e.Passport == 123456).First();
            var newAmount = 200;

            if (!employee.Accounts.ContainsKey("USD"))
            {
                employee.Accounts["USD"] = new List<Account> { new Account { Currency = new Currency { Code = "USD", Name = "Dollar" }, Amount = 0 } };
            }

            // Act
            _employeeService.EditAccount(employee.Passport, "USD", newAmount);

            // Assert
            Assert.Equal(newAmount, employee.Accounts["USD"][0].Amount);
        }

        [Fact]
        public void AddAdditionalAccount_ValidData_ShouldAddAdditionalAccount()
        {
            // Arrange
            var employee = _employeeStorage.Get(e => e.Passport == 123456).First();
            var newAccount = new Account
            {
                Currency = new Currency { Code = "EUR", Name = "Euro" },
                Amount = 100
            };

            // Act
            _employeeService.AddAdditionalAccount(employee.Passport, newAccount);

            // Assert
            Assert.True(employee.Accounts.ContainsKey("EUR"));
            Assert.Single(employee.Accounts["EUR"]);
            Assert.Equal(100, employee.Accounts["EUR"][0].Amount);
        }

        [Fact]
        public void GetFilteredEmployees_WithValidName_ShouldReturnEmployees()
        {
            // Act
            var result = _employeeStorage.Get(e => e.Name == "Alice");

            // Assert
            Assert.Single(result);
            Assert.Equal("Alice", result.First().Name);
        }

        [Fact]
        public void GetFilteredEmployees_WithNonExistentName_ShouldReturnNoEmployees()
        {
            // Act
            var result = _employeeStorage.Get(e => e.Name == "NonExistentName");

            // Assert
            Assert.Empty(result);
        }
    }*/
}
