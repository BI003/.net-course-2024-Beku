using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BankSystemDbContext>
    {
        public BankSystemDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BankSystemDbContext>();

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=BankSystemDb;Username=postgres;Password=admin");

            return new BankSystemDbContext(optionsBuilder.Options);
        }
    }
}