using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaServerDemo.Model;

namespace PizzaServerDemo.Data
{
    public class PizzaServerDemoContext : DbContext
    {
        public PizzaServerDemoContext (DbContextOptions<PizzaServerDemoContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Model.PizzaOrder>().HasKey(sc => new { sc.PizzaID, sc.OrderID });
        }

        public DbSet<PizzaServerDemo.Model.Order> Order { get; set; }

        public DbSet<PizzaServerDemo.Model.Pizza> Pizza { get; set; }

        public DbSet<PizzaServerDemo.Model.PizzaOrder> PizzaOrder { get; set; }


    }
}
