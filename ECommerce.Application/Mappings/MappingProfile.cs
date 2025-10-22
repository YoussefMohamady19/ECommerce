using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Domain.Entities;
using ECommerce.Application.DTOs;
using System.Linq;

namespace ECommerce.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Customer
            CreateMap<Customer, CustomerDto>();
            CreateMap<CreateCustomerDto, Customer>();

            // Product
            CreateMap<Product, ProductDto>();

            // Order -> OrderDto (map items)
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.Name : string.Empty))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderProducts.Select(op =>
                    new OrderItemDetailDto(op.ProductId, op.Product != null ? op.Product.Name : string.Empty, op.Quantity, op.Product != null ? op.Product.Price : 0)
                ).ToList()));

            // Order -> OrderSummaryDto
            CreateMap<Order, OrderSummaryDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.Name : string.Empty))
                .ForMember(dest => dest.NumberOfProducts, opt => opt.MapFrom(src => src.OrderProducts != null ? src.OrderProducts.Sum(op => op.Quantity) : 0));
        }
    }
}
