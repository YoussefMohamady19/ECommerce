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
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<CustomerDto> CreateAsync(CreateCustomerDto dto)
        {
            // map dto -> entity
            var entity = _mapper.Map<Customer>(dto);

            // add
            await _uow.Customers.AddAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<CustomerDto>(entity);
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var list = await _uow.Customers.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(list);
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var entity = await _uow.Customers.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<CustomerDto>(entity);
        }
    }
}
