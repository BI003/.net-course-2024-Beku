using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using Xunit;

namespace BancSystem.App.Tests
{
    public class EmployeeStorageTest
    {
        private EmployeeStorage InitializeStorageWithEmployees()
        {
            var storage = new EmployeeStorage();
            var employees = new List<Employee>()
            {
                new Employee { Name = "Алексей", Surname = "Иванов", Passport = 123456, Age = 30, Contract = "C001", Salary = 50000 },
                new Employee { Name = "Марина", Surname = "Петрова", Passport = 124565, Age = 25, Contract = "C002", Salary = 45000 },
                new Employee { Name = "Сергей", Surname = "Сидоров", Passport = 857441, Age = 40, Contract = "C003", Salary = 70000 },
                new Employee { Name = "Елена", Surname = "Кузнецова", Passport = 893615, Age = 22, Contract = "C004", Salary = 55000 },
                new Employee { Name = "Николай", Surname = "Федоров", Passport = 789123, Age = 35, Contract = "C005", Salary = 60000 }
            };

            foreach (var employee in employees)
            {
                storage.AddEmployee(employee);
            }
            return storage;
        }

        [Fact]
        public void AddEmployee_EmployeeDoesNotExist_EmployeeAddedSuccessfully()
        {
            // Arrange
            var storage = new EmployeeStorage();
            var employees = new List<Employee>()
            {
                new Employee { Name = "Алексей", Surname = "Иванов", Passport = 123456, Age = 30, Contract = "C001", Salary = 50000 },
                new Employee { Name = "Марина", Surname = "Петрова", Passport = 124565, Age = 25, Contract = "C002", Salary = 45000 },
                new Employee { Name = "Сергей", Surname = "Сидоров", Passport = 857441, Age = 40, Contract = "C003", Salary = 70000 },
                new Employee { Name = "Елена", Surname = "Кузнецова", Passport = 893615, Age = 22, Contract = "C004", Salary = 55000 },
                new Employee { Name = "Николай", Surname = "Федоров", Passport = 789123, Age = 35, Contract = "C005", Salary = 60000 }
            };

            // Act
            foreach (var employee in employees)
            {
                storage.AddEmployee(employee);
            }

            // Assert
            Assert.Equal(5, storage.GetAllEmployees().Count());
        }

        [Fact]
        public void GetYoungestEmployee_ReturnsCorrectEmployee()
        {
            // Arrange
            var storage = InitializeStorageWithEmployees();

            // Act
            var youngestEmployee = storage.GetYoungestEmployee();

            // Assert
            Assert.Equal(("Елена", 22), (youngestEmployee.Name, youngestEmployee.Age));
        }

        [Fact]
        public void GetOldestEmployee_ReturnsCorrectEmployee()
        {
            // Arrange
            var storage = InitializeStorageWithEmployees();

            // Act
            var oldestEmployee = storage.GetOldestEmployee();

            // Assert
            Assert.Equal(("Сергей", 40), (oldestEmployee.Name, oldestEmployee.Age));
        }

        [Fact]
        public void GetAverageAge_ReturnsCorrectAverage()
        {
            // Arrange
            var storage = InitializeStorageWithEmployees();

            // Act
            var averageAge = storage.GetAverageAge();

            // Assert
            Assert.Equal(30.4, averageAge);
        }
    }
}
