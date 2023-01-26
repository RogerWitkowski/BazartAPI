using Bazart.Models.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bazart.DataAccess.Configuration
{
    internal class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> entityTypeBuilder)
        {
            entityTypeBuilder
                .Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();
            entityTypeBuilder
                .Property(e => e.Description)
                .HasMaxLength(200)
                .IsRequired();
            entityTypeBuilder
                .HasOne(e => e.EventDetails)
                .WithOne(ed => ed.Event)
                .HasForeignKey<EventDetail>(key => key.EventId);
        }
    }
}