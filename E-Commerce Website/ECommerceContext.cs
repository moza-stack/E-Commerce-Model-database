using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using E_Commerce_Website.Models;

namespace E_Commerce_Website
{
    public class ECommerceContext : DbContext
    
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=ALMAZIDI;Database=ECommerceDB;Trusted_Connection=True;TrustServerCertificate=True;")
                .UseLazyLoadingProxies();
        }

    }
}
