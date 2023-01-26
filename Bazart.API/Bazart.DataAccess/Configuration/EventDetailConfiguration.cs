using Bazart.Models.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bazart.DataAccess.Configuration
{
    internal class EventDetailConfiguration : IEntityTypeConfiguration<EventDetail>
    {
        public void Configure(EntityTypeBuilder<EventDetail> entityTypeBuilder)
        {
            entityTypeBuilder
                .Property(ed => ed.Country)
                .HasMaxLength(50)
                .IsRequired();
            entityTypeBuilder
                .Property(ed => ed.City)
                .HasMaxLength(50)
                .IsRequired();
            entityTypeBuilder
                .Property(ed => ed.Street)
                .HasMaxLength(50)
                .IsRequired();
            entityTypeBuilder
                .Property(ed => ed.City)
                .HasMaxLength(50)
                .IsRequired();
            entityTypeBuilder
                .Property(ed => ed.HouseOrFlatNumber)
                .HasMaxLength(10)
                .IsRequired();
            entityTypeBuilder
                .Property(ed => ed.PostalCode)
                .HasMaxLength(10)
                .IsRequired();
            entityTypeBuilder
                .Property(ed => ed.ImageUrl)
                .IsRequired();
        }
    }
}