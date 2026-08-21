using System;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class ReviewConfig : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasIndex(review => new { review.ApplicationUserId, review.ProductId })
                .IsUnique();
        builder.Property(review => review.TheReview)
                .HasMaxLength(350);
    }
}
