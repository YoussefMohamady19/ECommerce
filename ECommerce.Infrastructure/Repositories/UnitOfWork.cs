using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
using ECommerce.Application.Interfaces;

namespace ECommerce.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ECommerceDbContext _context;
        public IGenericRepository<Product> Products { get; }
        public IGenericRepository<Customer> Customers { get; }
        public IOrderRepository Orders { get; } 

        public UnitOfWork(ECommerceDbContext context)
        {
            _context = context;
            Products = new GenericRepository<Product>(_context);
            Customers = new GenericRepository<Customer>(_context);
            Orders = new OrderRepository(_context); 
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
