using Bazart.Models.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bazart.DataAccess.Configuration
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entityTypeBuilder)
        {
            entityTypeBuilder
                .Property(c => c.Name)
                .HasMaxLength(19)
                .IsRequired();
            entityTypeBuilder
                .Property(c => c.Description)
                .HasMaxLength(50);
            //    .IsRequired();
            //entityTypeBuilder
            //    .Property(c => c.ImageUrl)
            //    .IsRequired();

            entityTypeBuilder
                .HasMany(c => c.Products)
                .WithOne(c => c.Category)
                .HasForeignKey(key => key.CategoryId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}