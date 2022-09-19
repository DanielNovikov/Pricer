using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Data.Persistent;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<Item> Items { get; set; }
        
    public DbSet<ItemPriceChange> ItemPriceChanges { get; set; }
    
    public DbSet<ItemParseResult> ItemParseResults { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<UserToken> UserTokens { get; set; }

    public DbSet<AppNotification> AppNotifications { get; set; }

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
    //    optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=PricerDB;Username=postgres;Password=postgres");
    // }

    public void DetachEntity<T>(T entity)
    {
        Entry(entity).State = EntityState.Detached;
    }
}