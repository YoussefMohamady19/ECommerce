using System.Net;
using System.Net.Http.Json;
using ECommerce.API; 
using ECommerce.Application.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ECommerce.Tests
{
    public class CustomersControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CustomersControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "POST /api/customers => 201 Created when valid data")]
        public async Task CreateCustomer_ShouldReturn201Created_WhenDataIsValid()
        {
            // Arrange
            var newCustomer = new CreateCustomerDto("Test User", "testuser@example.com", "01000000000");

            // Act
            var response = await _client.PostAsJsonAsync("/api/customers", newCustomer);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var created = await response.Content.ReadFromJsonAsync<CustomerDto>();
            created.Should().NotBeNull();
            created!.Name.Should().Be("Test User");
            created.Email.Should().Be("testuser@example.com");
        }

        [Fact(DisplayName = "POST /api/customers => 400 BadRequest when invalid data")]
        public async Task CreateCustomer_ShouldReturnBadRequest_WhenDataIsInvalid()
        {
            // Arrange: missing name + invalid email
            var invalidCustomer = new CreateCustomerDto("", "not-an-email", null);

            // Act
            var response = await _client.PostAsJsonAsync("/api/customers", invalidCustomer);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "GET /api/customers/{id} => 200 OK when exists")]
        public async Task GetCustomer_ShouldReturn200_WhenCustomerExists()
        {
            // Arrange: create one first
            var customer = new CreateCustomerDto("Existing User", "existing@example.com", "01111111111");
            var createResponse = await _client.PostAsJsonAsync("/api/customers", customer);
            var created = await createResponse.Content.ReadFromJsonAsync<CustomerDto>();

            // Act
            var response = await _client.GetAsync($"/api/customers/{created!.Id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var fetched = await response.Content.ReadFromJsonAsync<CustomerDto>();
            fetched!.Name.Should().Be("Existing User");
        }

        [Fact(DisplayName = "GET /api/customers/{id} => 404 NotFound when not exists")]
        public async Task GetCustomer_ShouldReturnNotFound_WhenCustomerDoesNotExist()
        {
            // Act
            var response = await _client.GetAsync("/api/customers/9999");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact(DisplayName = "GET /api/customers => 200 OK returns list")]
        public async Task GetAllCustomers_ShouldReturnList()
        {
            // Arrange: Add two customers
            await _client.PostAsJsonAsync("/api/customers", new CreateCustomerDto("A", "a@a.com", null));
            await _client.PostAsJsonAsync("/api/customers", new CreateCustomerDto("B", "b@b.com", null));

            // Act
            var response = await _client.GetAsync("/api/customers");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var list = await response.Content.ReadFromJsonAsync<List<CustomerDto>>();
            list.Should().NotBeNull();
            list!.Count.Should().BeGreaterThanOrEqualTo(2);
        }
    }
}
