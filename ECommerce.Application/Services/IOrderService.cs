using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Application.DTOs;

namespace ECommerce.Application.Services
{
    public interface IOrderService
    {
        Task<OrderSummaryDto> CreateOrderAsync(CreateOrderDto dto);
        Task<OrderDto?> GetOrderByIdAsync(int id);
        Task<bool> UpdateOrderStatusToDeliveredAsync(int id);
    }
}
