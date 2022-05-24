using System;
using ComputerShopDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerShopDatabaseImplement
{
    public class ComputerShopDatabase : DbContext

    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=ComputerShopDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Assembly> Assemblies { set; get; }
        public virtual DbSet<AssemblyOrder> AssemblyOrders { set; get; }
        public virtual DbSet<Component> Components { set; get; }
        public virtual DbSet<DeliveryComponent> DeliveryComponents { set; get; }
        public virtual DbSet<Customer> Customers { set; get; }
        public virtual DbSet<Delivery> Deliveries { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<Receiving> Receivings { set; get; }
    }
}
