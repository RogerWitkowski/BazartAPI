using Bazart.Models.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bazart.DataAccess.Configuration
{
    internal class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> entityTypeBuilder)
        {
            entityTypeBuilder
                .Property(a => a.Country)
                .HasMaxLength(50)
                .IsRequired();
            entityTypeBuilder
                .Property(a => a.City)
                .HasMaxLength(50)
                .IsRequired();
            entityTypeBuilder
                .Property(a => a.Street)
                .HasMaxLength(50)
                .IsRequired();
            entityTypeBuilder
                .Property(a => a.HouseOrFlatNumber)
                .HasMaxLength(10)
                .IsRequired();
            entityTypeBuilder
                .Property(a => a.PostalCode)
                .HasMaxLength(10)
                .IsRequired();
            entityTypeBuilder
                .HasOne(u => u.User)
                .WithOne(a => a.Address)
                .HasForeignKey<User>(key => key.AddressId);
        }
    }
}