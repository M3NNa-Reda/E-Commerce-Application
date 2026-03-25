using E_Commerce_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce_Application.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers", "shop");
            
            builder.HasKey(p => p.CustomerId)
                .HasName("customer_id");

            builder.Property(p => p.FullName)
                .HasColumnName("full_name")
                .IsRequired()
                .HasMaxLength(150)
                .HasComment("Customer full legal name");

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(250);
            builder.HasIndex(e => e.Email)
                .HasDatabaseName("IX_Customers_Email")
                .IsUnique();

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
