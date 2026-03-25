using E_Commerce_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce_Application.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        void IEntityTypeConfiguration<Product>.Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", "shop");
            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
            builder.Property(p => p.IsActive)
                .HasDefaultValue(true);
            builder.Property(p => p.DisplayName)
                .HasComputedColumnSql(" [Name] + ' (' + [SKU] + ')' ", stored: true);
            builder.HasQueryFilter(p => p.IsActive);

            builder.HasIndex(p=>p.SKU)
                .HasDatabaseName(" IX_Products_SKU")
                .IsUnique();
            builder.HasOne(p => p.Category)
                .WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
