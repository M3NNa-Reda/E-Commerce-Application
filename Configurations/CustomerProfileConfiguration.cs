using E_Commerce_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce_Application.Configurations
{
    public class CustomerProfileConfiguration : IEntityTypeConfiguration<CustomerProfile>
    {
        public void Configure(EntityTypeBuilder<CustomerProfile> builder)
        {
            builder.ToTable("CustomerProfiles", "shop");

            builder.Property(p=>p.Address).HasMaxLength(300);
            builder.Property(p => p.City).HasMaxLength(100);
            builder.Property(p => p.PostalCode).HasMaxLength(20);
            builder.Property(p => p.NationalId)
                .HasColumnType("nchar(14)")
                .HasMaxLength(30);

            builder.HasOne(p => p.Customer)
                .WithOne(p => p.CustomerProfile)
                .HasForeignKey<CustomerProfile>(cp => cp.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
