using BankSystem.Data.Storages;

namespace BankSystem.App.Services
{
    public class EmployeeService
    {
        private readonly EmployeeStorage _employeeStorage;

        public EmployeeService(EmployeeStorage employeeStorage)
        {
            _employeeStorage = employeeStorage;
        }
    }
}
