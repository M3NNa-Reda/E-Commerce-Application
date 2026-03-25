using E_Commerce_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce_Application.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders", "shop");
            builder.Property(p => p.Status)
                .HasConversion<string>()
                .HasMaxLength(30)
                .HasDefaultValue(OrderStatus.Pending);
            builder.Property(p => p.TotalAmount)
                .HasColumnType("decimal(18,2)");
            builder.Property(p => p.PlacedAt)
                .HasDefaultValueSql("GETUTCDATE()");
            builder.HasIndex(p => p.Status)
                .HasFilter("[Status] = 'Pending'")
                .HasDatabaseName("IX_Orders_PendingStatus");
            builder.HasOne(p => p.Customer)
                .WithMany(p => p.Orders)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
