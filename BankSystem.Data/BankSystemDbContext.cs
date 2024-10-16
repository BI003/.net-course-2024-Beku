using BankSystem.Data.EntityConfigurations;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data
{
    public class BankSystemDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public BankSystemDbContext(DbContextOptions<BankSystemDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(
                    "Host=localhost;Port=5432;Database=BankSystemDb;Username=postgres;Password=admin"
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AccountEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
