using Bazart.Models.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bazart.DataAccess.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entityTypeBuilder)
        {
            entityTypeBuilder
                .Property(u => u.FirstName)
                .HasMaxLength(20)
                .IsRequired();
            entityTypeBuilder
                .Property(u => u.LastName)
                .HasMaxLength(30)
                .IsRequired();
            entityTypeBuilder
                .Property(u => u.Email)
                .IsRequired();
            entityTypeBuilder
                .Property(u => u.DateOfBirth)
                .IsRequired();
            entityTypeBuilder
                .Property(u => u.Nationality)
                .IsRequired();
        }
    }
}