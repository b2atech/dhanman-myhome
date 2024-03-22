using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class UserRoles : IEntityTypeConfiguration<Domain.Authorization.UserPaidModules>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Domain.Authorization.UserPaidModules> builder)
    {
        builder.ToTable(TableNames.UserRoles);

        builder.HasKey(m => m.UserId);

        builder.Property(m => m.PaidModules).IsRequired();
    }
}