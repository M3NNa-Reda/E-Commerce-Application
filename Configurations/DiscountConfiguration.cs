using E_Commerce_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce_Application.Configurations
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("Discounts", "shop");
            builder.Property(p => p.DiscountId)
                .HasDefaultValueSql("NEXT VALUE FOR shop.DiscountSeq");
            builder.Property(p => p.Code)
                .IsRequired()
                .HasMaxLength(30);
            builder.HasIndex(p => p.Code)
                .HasDatabaseName("IX_Discounts_Code")
                .IsUnique();
            builder.Property(p => p.Percentage)
                .HasColumnType("decimal(5,2)");
            builder.Property(p => p.IsActive)
                .HasDefaultValue(true);
            builder.Property(p => p.MaxUses)
                .HasDefaultValue(100);
        }
    }
}
