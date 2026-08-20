using System;
using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces;

public interface IOrderRepository : IGenericRepository<OrderHeader>
{
    IEnumerable<OrderHeader> GetCustomerOrdersWithDetails(string customerId);
}
