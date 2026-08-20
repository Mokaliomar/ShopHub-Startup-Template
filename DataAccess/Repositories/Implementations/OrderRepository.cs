using System;
using System.Security.Cryptography;
using DataAccess.Data;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations;

public class OrderRepository : GenericRepository<OrderHeader>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context) : base(context)
    { }

    public IEnumerable<OrderHeader> GetCustomerOrdersWithDetails(string customerId)
    {
        return dbSet.Include(orderHeader => orderHeader.OrderDetails)
            .ThenInclude(orderDetail => orderDetail.Product)
            .Where(orderHeader => orderHeader.ApplicationUserId == customerId);
    }
}
