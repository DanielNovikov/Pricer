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

        public DbSet<Menu> Menus { get; set; }
        
        public DbSet<MenuCommand> MenuCommands { get; set; }

        public DbSet<Command> Commands { get; set; }

        public ObserverContext()
        {
        }
        
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = ObserverDatabase.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public void DetachAll()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity != null)
                    entry.State = EntityState.Detached;
            }
        }
    }
}