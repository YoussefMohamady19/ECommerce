using System;
using System.Threading.Tasks;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Product> Products { get; }
        IGenericRepository<Customer> Customers { get; }
        IOrderRepository Orders { get; } 
        Task<int> CommitAsync();
    }

}
