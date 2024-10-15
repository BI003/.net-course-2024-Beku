using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Data.EntityConfigurations
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(c => c.Id);
            builder.Property(e => e.Name).HasMaxLength(20).IsRequired();
            builder.Property(e => e.Surname).HasMaxLength(20).IsRequired();
            builder.Property(e => e.Age);
            builder.Property(e => e.Passport).IsRequired();
            builder.Property(e => e.PhoneNumber);
            builder.Property(e => e.DateOfBirth);

            builder.Property(e => e.Contract).IsRequired();
            builder.Property(e => e.Salary).IsRequired();

            builder.HasMany(e => e.Accounts).WithOne().OnDelete(DeleteBehavior.Cascade);

        }
    }
}
