using E_Commerce_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce_Application.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews", "shop");
            builder.Property(p => p.Rating).IsRequired();
            builder.Property(p => p.Comment)
                .IsRequired(false)
                .HasMaxLength(1000);
            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
            builder.HasIndex(i => new { i.ProductId, i.CustomerId })
                .HasDatabaseName("IX_Reviews_Product_Customer");
            builder.HasOne(p=>p.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Customer)
                .WithMany(p => p.Reviews)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
