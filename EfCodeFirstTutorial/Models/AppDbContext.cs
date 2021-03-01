using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace EfCodeFirstTutorial.Models {
    
    public class AppDbContext : DbContext {

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Orderline> Orderlines { get; set; }

        public AppDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            if(!builder.IsConfigured) {
                var connStr = "server=localhost\\sqlexpress;" +
                                "database=AppDb1;" +
                                "trusted_connection=true;";
                builder.UseSqlServer(connStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<Customer>(cust => {
                cust.HasIndex(x => x.Code).IsUnique(true);
            });
        }
    }
}
