using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data.Models;

namespace PriceObserver.Data
{
    public sealed class ApplicationDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        
        public DbSet<ItemPriceChange> ItemPriceChanges { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserToken> UserTokens { get; set; }

        public DbSet<Menu> Menus { get; set; }
        
        public DbSet<MenuCommand> MenuCommands { get; set; }

        public DbSet<Command> Commands { get; set; }
        
        public DbSet<Shop> Shops { get; set; }

        public DbSet<AppNotification> AppNotifications { get; set; }
        
        public DbSet<Resource> Resources { get; set; }

        public ApplicationDbContext()
        {
        }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        // used to generate migrations
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //    optionsBuilder.UseNpgsql("Host=localhost;Port=1488;Database=PricerDB;Username=postgres;Password=postgres");
        // }

        public void DetachAll()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is not null)
                    entry.State = EntityState.Detached;
            }
        }
    }
}