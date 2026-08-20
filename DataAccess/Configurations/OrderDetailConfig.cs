using System;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class OrderDetailConfig : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.HasKey(orderDetail => orderDetail.Id);

        builder.Property(orderDetail => orderDetail.Price).HasPrecision(18,2);

        builder.HasOne(orderDetail => orderDetail.Product)
                .WithMany(product => product.OrderDetails)
                .HasForeignKey(orderDetail => orderDetail.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
    }
}
