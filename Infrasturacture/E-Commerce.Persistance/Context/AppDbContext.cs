using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistance.Context
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=Enes;Database=ECommerceDb;Integrated Security=true;TrustServerCertificate=True");
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Product -> Category
        //    modelBuilder.Entity<Product>()
        //        .HasOne(p => p.Category)
        //        .WithMany(c => c.Products)
        //        .HasForeignKey(p => p.CategoryID);
        //    modelBuilder.Entity<Category>()
        //        .HasMany(c => c.Products)
        //        .WithOne(p => p.Category)
        //        .HasForeignKey(p => p.CategoryID);

        //    // Customer -> Order
        //    modelBuilder.Entity<Customer>()
        //        .HasMany(c => c.Orders)
        //        .WithOne(o => o.Customer)
        //        .HasForeignKey(o => o.CustomerID);

        //    // Order -> OrderItem
        //    modelBuilder.Entity<Order>()
        //        .HasMany(o => o.OrderItems)
        //        .WithOne(oi => oi.Order)
        //        .HasForeignKey(oi => oi.OrderID);
        //    modelBuilder.Entity<Order>()
        //         .HasOne(o => o.Customer)
        //         .WithMany(c => c.Orders)
        //         .HasForeignKey(o => o.CustomerID);
        //    modelBuilder.Entity<OrderItem>()
        //        .HasOne(oi => oi.Order)
        //        .WithMany(p => p.OrderItems)
        //        .HasForeignKey(oi => oi.OrderID);
        //}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

    }
}
