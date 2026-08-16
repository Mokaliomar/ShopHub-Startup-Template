using System;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

        builder.Property(c => c.Description).HasMaxLength(500);
        
        builder.HasIndex(c => c.Name).IsUnique();
    }
}
