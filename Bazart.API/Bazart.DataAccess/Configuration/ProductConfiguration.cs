using Bazart.Models.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bazart.DataAccess.Configuration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entityTypeBuilder)
        {
            entityTypeBuilder
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();
            entityTypeBuilder
                .Property(p => p.Description)
                .HasMaxLength(200)
                .IsRequired();
            entityTypeBuilder
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            entityTypeBuilder
                .Property(p => p.IsForSell)
                .IsRequired();
            entityTypeBuilder
                .Property(p => p.ImageUrl)
                .IsRequired();
            entityTypeBuilder
                .HasOne(p => p.CreatedBy)
                .WithMany(u => u.Products)
                .HasForeignKey(p => p.CreatedById);

            entityTypeBuilder
                .HasOne(c => c.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(key => key.CategoryId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}