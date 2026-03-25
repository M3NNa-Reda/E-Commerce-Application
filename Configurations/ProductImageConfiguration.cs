using E_Commerce_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce_Application.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages", "shop");
            builder.Property(p => p.Url)
                .HasMaxLength(500)
                .IsRequired();
            builder.Property(p => p.AltText)
                .HasMaxLength(200)
                .IsRequired(false);
            builder.Property(p => p.IsPrimary)
                .HasDefaultValue(false);
            builder.HasOne(p => p.Product)
                .WithOne(p => p.ProductImage)
                .HasForeignKey<ProductImage>(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
