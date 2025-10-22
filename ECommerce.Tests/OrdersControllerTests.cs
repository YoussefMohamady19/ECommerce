using ECommerce.API;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;

namespace ECommerce.Tests
{
    public class OrdersControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public OrdersControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetOrder_ShouldReturnNotFound_WhenOrderDoesNotExist()
        {
            var response = await _client.GetAsync("/api/orders/999");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
