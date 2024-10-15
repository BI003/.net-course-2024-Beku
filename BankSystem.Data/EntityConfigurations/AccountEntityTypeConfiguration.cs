using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Data.EntityConfigurations
{
    public class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder) 
        {
            builder.ToTable("Accounts");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.CurrencyName).IsRequired();
            builder.Property(a => a.Amount).HasColumnType("decimal(18,2)");
        }
    }
}
