using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Model.Data;

namespace PriceObserver.Data
{
    public class ObserverContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        
        public DbSet<User> Users { get; set; }

        public ObserverContext(DbContextOptions<ObserverContext> options) : base(options)
        {
            try
            {
                Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                
            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}