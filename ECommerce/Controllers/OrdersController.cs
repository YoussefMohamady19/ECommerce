using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Services;
using Microsoft.AspNetCore.Mvc;
namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // POST: api/orders
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
        {
            try
            {
                var created = await _orderService.CreateOrderAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // GET: api/orders/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound(new { Message = "Order not found" });

            return Ok(order);
        }

        // PUT: api/orders/{id}/deliver
        [HttpPut("{id}/deliver")]
        public async Task<IActionResult> MarkAsDelivered(int id)
        {
            var updated = await _orderService.UpdateOrderStatusToDeliveredAsync(id);
            if (!updated)
                return NotFound(new { Message = "Order not found" });

            return Ok(new { Message = "Order marked as delivered" });
        }
    }

}
