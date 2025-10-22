using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs
{
    public record OrderItemDto(int ProductId, int Quantity);

    public record CreateOrderDto(int CustomerId, List<OrderItemDto> Items);

    public record OrderSummaryDto(int Id, string CustomerName, string Status, int NumberOfProducts);

    public record OrderDto(
        int Id,
        int CustomerId,
        string CustomerName,
        DateTime OrderDate,
        string Status,
        double TotalPrice,
        IReadOnlyList<OrderItemDetailDto> Items
    );

    public record OrderItemDetailDto(int ProductId, string ProductName, int Quantity, double UnitPrice);
}
