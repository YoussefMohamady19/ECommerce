using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ECommerce.Domain.Entities;

namespace ECommerce.Infrastructure.Data
{
    public class ECommerceDbContext:DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options) { }


        public DbSet<Product> Products => Set<Product>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderProduct> OrderProducts => Set<OrderProduct>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<OrderProduct>().HasKey(op => new { op.OrderId, op.ProductId });


            modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Order)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(op => op.OrderId);


            modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Product)
            .WithMany(p => p.OrderProducts)
            .HasForeignKey(op => op.ProductId);


            // Seed sample products
            modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Laptop", Description = "A nice laptop", Price = 1000, Stock = 10 },
            new Product { Id = 2, Name = "Mouse", Description = "Wireless mouse", Price = 25, Stock = 100 },
            new Product { Id = 3, Name = "Keyboard", Description = "Mechanical keyboard", Price = 75, Stock = 50 }
            );
        }
    }
}
