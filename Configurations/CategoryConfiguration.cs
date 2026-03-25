using E_Commerce_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce_Application.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        void IEntityTypeConfiguration<Category>.Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories", "shop");
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.Slug)
                .IsRequired()
                .HasMaxLength(120);
            builder.HasIndex(p => p.Slug)
                .HasDatabaseName("IX_Categories_Slug")
                .IsUnique();
            builder.Ignore(p => p.InternalNotes);
            builder.HasOne(p => p.ParentCategory)
                .WithMany(p => p.SubCategories)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
