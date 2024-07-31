using Dhanman.MyHome.Domain.Entities.Users;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.Users);

        builder.HasKey(user => user.Id);

        builder.OwnsOne(user => user.FirstName, firstNameBuilder =>
        {
            firstNameBuilder.WithOwner();

            firstNameBuilder.Property(firstName => firstName.Value)
                .HasColumnName("first_name")
                .HasMaxLength(FirstName.MaxLength)
                .IsRequired();
        });

        builder.OwnsOne(user => user.LastName, lastNameBuilder =>
        {
            lastNameBuilder.WithOwner();

            lastNameBuilder.Property(lastName => lastName.Value)
                .HasColumnName("last_name")
                .HasMaxLength(LastName.MaxLength)
                .IsRequired();
        });

        builder.OwnsOne(user => user.Email, emailBuilder =>
        {
            emailBuilder.WithOwner();

            emailBuilder.Property(email => email.Value)
                .HasColumnName("email")
                .HasMaxLength(Email.MaxLength)
                .IsRequired();
        });

        builder.OwnsOne(user => user.ContactNumber, contactNumberBuilder =>
        {
            contactNumberBuilder.WithOwner();

            contactNumberBuilder.Property(contactNumber => contactNumber.Value)
                .HasColumnName("contact_number")
                .HasMaxLength(ContactNumber.MaxLength)
                .IsRequired();
        });

        builder.Property(user => user.IsOwner)
            .HasColumnName("is_owner")
            .IsRequired();

        builder.Property<string>("_passwordHash")
            .HasField("_passwordHash")
            .HasColumnName("password_hash")
            .IsRequired();

        builder.Property(user => user.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(user => user.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(user => user.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(user => user.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(user => !user.IsDeleted);
    }
}