using System;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);

        builder.Property(p => p.Description).HasMaxLength(500);

        builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");

        builder.Property(p => p.CategoryId).IsRequired();


        builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(p => p.Name);
    }
}
