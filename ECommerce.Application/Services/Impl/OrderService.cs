using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;
using ECommerce.Application.Interfaces;


namespace ECommerce.Application.Services.Impl
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<OrderSummaryDto> CreateOrderAsync(CreateOrderDto dto)
        {
            var customer = await _uow.Customers.GetByIdAsync(dto.CustomerId)
                           ?? throw new InvalidOperationException($"Customer {dto.CustomerId} not found.");

            if (dto.Items == null || !dto.Items.Any())
                throw new InvalidOperationException("Order must contain at least one product.");

            var productIds = dto.Items.Select(i => i.ProductId).ToList();
            var products = (await _uow.Products.FindAsync(p => productIds.Contains(p.Id))).ToList();

            foreach (var item in dto.Items)
            {
                var p = products.First(x => x.Id == item.ProductId);
                if (p.Stock < item.Quantity)
                    throw new InvalidOperationException($"Insufficient stock for {p.Name}");
            }

            var order = new Order
            {
                CustomerId = dto.CustomerId,
                OrderDate = DateTime.UtcNow,
                Status = "Pending",
                TotalPrice = dto.Items.Sum(i => products.First(p => p.Id == i.ProductId).Price * i.Quantity),
                OrderProducts = dto.Items.Select(i => new OrderProduct
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                }).ToList()
            };

            foreach (var item in dto.Items)
            {
                var p = products.First(x => x.Id == item.ProductId);
                p.Stock -= item.Quantity;
                _uow.Products.Update(p);
            }

            await _uow.Orders.AddAsync(order);
            await _uow.CommitAsync();

            return new OrderSummaryDto(order.Id, customer.Name, order.Status, order.OrderProducts.Sum(op => op.Quantity));
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            var order = await _uow.Orders.GetOrderWithDetailsAsync(id);
            if (order == null) return null;

            var dto = new OrderDto(
                order.Id,
                order.CustomerId,
                order.Customer?.Name ?? string.Empty,
                order.OrderDate,
                order.Status,
                order.TotalPrice,
                order.OrderProducts.Select(op =>
                    new OrderItemDetailDto(op.ProductId, op.Product?.Name ?? "", op.Quantity, op.Product?.Price ?? 0)
                ).ToList()
            );

            return dto;
        }

        public async Task<bool> UpdateOrderStatusToDeliveredAsync(int id)
        {
            var order = await _uow.Orders.GetByIdAsync(id);
            if (order == null) return false;

            order.Status = "Delivered";
            _uow.Orders.Update(order);
            await _uow.CommitAsync();
            return true;
        }
    }
}

