using E_Commerce_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce_Application.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments", "shop");
            builder.Property(p=>p.Method).IsRequired();
            builder.Property(p => p.Status)
                .HasConversion<string>()
                .HasMaxLength(30);
            builder.Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");
            builder.HasOne(p => p.Order)
                .WithOne(p => p.Payment)
                .HasForeignKey<Payment>(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
