using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Model.Data;

namespace PriceObserver.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        
        public DbSet<ItemPriceChange> ItemPriceChanges { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Menu> Menus { get; set; }
        
        public DbSet<MenuCommand> MenuCommands { get; set; }

        public DbSet<Command> Commands { get; set; }
        
        public DbSet<Shop> Shops { get; set; }

        public ApplicationDbContext()
        {
        }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database?.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=1488;Database=PricerDB;Username=postgres;Password=postgres");
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