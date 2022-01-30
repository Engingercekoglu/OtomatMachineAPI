using Microsoft.EntityFrameworkCore;
using OtomatMachine.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
namespace OtomatMachine.Entity.Context
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

        }
        public DataContext(DbContextOptions<DataContext> options) 
            :base(options)
        {
            
        }

        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType > ProductTypes { get; set; }
        public DbSet<ReceiptTransaction> ReciptTransactions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("A FALLBACK CONNECTION STRING");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
               .HasColumnType("decimal(18,4)");
            modelBuilder.Entity<ReceiptTransaction>()
              .Property(p => p.TotalPrice)
             .HasColumnType("decimal(18,4)");
            modelBuilder.Entity<ReceiptTransaction>()
             .Property(p => p.RefundedAmount)
            .HasColumnType("decimal(18,4)");
            base.OnModelCreating(modelBuilder);
        }
    }
}
