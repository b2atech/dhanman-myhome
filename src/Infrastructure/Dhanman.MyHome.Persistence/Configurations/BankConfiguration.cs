using Dhanan.MyHome.Domain.Entities.Banks;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class BankConfiguration : IEntityTypeConfiguration<Bank>
{
    #region Methodes
    public void Configure(EntityTypeBuilder<Bank> builder)
    {
        builder.ToTable(TableNames.Banks);
        builder.HasKey(bank => bank.Id);

        builder.Property(bank => bank.BankName).HasColumnName("bank_name").IsRequired();

        builder.Property(bank => bank.BranchName).HasColumnName("branch_name").IsRequired();

        builder.Property(bank => bank.AccountNumber).HasColumnName("account_number").IsRequired();

        builder.Property(bank => bank.IFSC).HasColumnName("ifsc").IsRequired();

        builder.Property(bank => bank.CreatedBy).HasColumnType("uuid");

        builder.Property(bank => bank.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(bank => bank.ModifiedBy).HasColumnType("uuid");

        builder.Property(bank => bank.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(bank => bank.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(bank => bank.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(bank => !bank.IsDeleted);
    }
    #endregion

}