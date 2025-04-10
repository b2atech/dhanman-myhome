using Dhanman.MyHome.Domain.Entities.Calendars;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class CalendarConfiguration : IEntityTypeConfiguration<Calendar>
{
    public void Configure(EntityTypeBuilder<Calendar> builder)
    {
        builder.ToTable(TableNames.Calendars); 

        builder.HasKey(calendar => calendar.Id);

        builder.Property(calendar => calendar.Name)
            .HasColumnName("name") 
            .IsRequired();

        builder.Property(calendar => calendar.Description)
            .HasColumnName("description"); 

        builder.Property(calendar => calendar.IsPublic)
            .HasColumnName("is_public")
            .IsRequired();

        builder.Property(calendar => calendar.CreatedBy)
            .HasColumnType("uuid") 
            .IsRequired();

        builder.Property(calendar => calendar.ModifiedBy)
            .HasColumnType("uuid");

        builder.Property(calendar => calendar.CreatedOnUtc)
            .HasColumnType("timestamp") 
            .IsRequired();

        builder.Property(calendar => calendar.ModifiedOnUtc)
            .HasColumnType("timestamp");

        builder.Property(calendar => calendar.DeletedOnUtc)
            .HasColumnType("timestamp");

        builder.Property(calendar => calendar.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.HasQueryFilter(calendar => !calendar.IsDeleted);
    }
}