using E_Commerce_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce_Application.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags", "shop");
            builder.Property(p=>p.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasIndex(p => p.Name)
                .IsUnique()
                .HasDatabaseName("IX_Tags_Name");
        }
    }
}
