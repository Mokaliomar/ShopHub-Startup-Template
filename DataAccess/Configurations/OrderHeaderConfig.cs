using System;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class OrderHeaderConfig : IEntityTypeConfiguration<OrderHeader>
{
    public void Configure(EntityTypeBuilder<OrderHeader> builder)
    {
        builder.HasKey(oh => oh.Id);

        builder.Property(oh => oh.TotalPrice).HasPrecision(18,2);

        builder.Property(oh => oh.Name).IsRequired().HasMaxLength(100);
        builder.Property(oh => oh.Address).IsRequired().HasMaxLength(250);
        builder.Property(oh => oh.City).IsRequired().HasMaxLength(50);
        builder.Property(oh => oh.PhoneNumber).IsRequired().HasMaxLength(20);

        builder.HasOne(orderHeader => orderHeader.ApplicationUser)
                .WithMany(applicationUser => applicationUser.OrderHeaders)
                .HasForeignKey(orderHeader => orderHeader.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(orderHeader => orderHeader.OrderDetails)
                .WithOne(orderDetail => orderDetail.OrderHeader)
                .HasForeignKey(orderDetail => orderDetail.OrderHeaderId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}
