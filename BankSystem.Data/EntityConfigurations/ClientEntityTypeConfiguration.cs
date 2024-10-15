using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Data.EntityConfigurations
{
    public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            builder.HasKey(c => c.Id);
            builder.Property(e => e.Name).HasMaxLength(20).IsRequired();
            builder.Property(e => e.Surname).HasMaxLength(20).IsRequired();
            builder.Property(e => e.Age);
            builder.Property(e => e.Passport).IsRequired();
            builder.Property(e => e.PhoneNumber);
            builder.Property(e => e.DateOfBirth);

            builder.HasMany(e => e.Accounts).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}