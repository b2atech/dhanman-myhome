using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.EventTypes;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class EventTypeConfiguration : IEntityTypeConfiguration<EventType>
{
    #region Methods

    public void Configure(EntityTypeBuilder<EventType> builder)
    {
        builder.ToTable(TableNames.EventTypes);

        builder.HasKey(eventTypeEntity => eventTypeEntity.Id);

        builder.Property(eventTypeEntity => eventTypeEntity.Name)
            .HasColumnName("name")
            .IsRequired();      
    }
        #endregion

}
