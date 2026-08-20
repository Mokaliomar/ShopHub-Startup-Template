using System;
using System.Data.Common;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;

namespace BusinessLogic.BL;

public class OrderManagement
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderManagement(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public bool CreateOrder(OrderHeader orderHeader, IEnumerable<OrderDetail> orderDetails)
    {
        if (orderHeader == null || orderDetails == null)
        {
            return false;
        }

        foreach (var orderDetail in orderDetails)
        {
            orderHeader.OrderDetails.Add(orderDetail);
        }
        _unitOfWork.OrderRepository.Create(orderHeader);

        _unitOfWork.Save();
        return true;
    }

    public IEnumerable<OrderHeader> GetCustomerOrdersWithDetails(string customerId)
    {
        var customerOrders = _unitOfWork.OrderRepository.GetCustomerOrdersWithDetails(customerId);
        return customerOrders;
    }
}
