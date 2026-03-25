using E_Commerce_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce_Application.Configurations
{
    public class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
    {
        public void Configure(EntityTypeBuilder<ProductTag> builder)
        {
            builder.ToTable("ProductTags", "shop");
            builder.HasKey(pt => new { pt.ProductId, pt.TagId });
            builder.HasOne(p => p.Product)
                .WithMany(p => p.productTags)
                .HasForeignKey(pt => pt.ProductId);
            builder.HasOne(p => p.Tag)
                .WithMany(p => p.productTags)
                .HasForeignKey(pt => pt.TagId);
        }
    }
}
