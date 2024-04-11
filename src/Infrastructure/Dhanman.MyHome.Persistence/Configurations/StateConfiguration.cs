using Dhanman.MyHome.Domain.Entities.States;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Configurations;

internal class StateConfiguration : IEntityTypeConfiguration<State>
{
    #region Methodes
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.ToTable(TableNames.States);
        builder.HasKey(states => states.Id);

        builder.Property(states => states.Name).HasColumnName("name").IsRequired();

        builder.Property(states => states.CountryId).HasColumnName("country_id").IsRequired();

        builder.Property(states => states.CreatedBy).HasColumnType("uuid");

        builder.Property(states => states.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(states => states.ModifiedBy).HasColumnType("uuid");

        builder.Property(states => states.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(states => states.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(states => states.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(states => !states.IsDeleted);
    }
    #endregion
}