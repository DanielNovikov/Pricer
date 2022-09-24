using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pricer.Data.Persistent.Models;

namespace Pricer.Data.Persistent.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(i => i.Id);

        builder
            .Property(i => i.Price)
            .IsRequired();

        builder
            .Property(i => i.Url)
            .HasConversion(
                v => v.ToString(),
                v => new Uri(v))
            .IsRequired();
            
        builder
            .Property(i => i.Title)
            .HasMaxLength(250)
            .IsRequired();

        builder
            .Property(i => i.ImageUrl)
            .HasConversion(
                v => v.ToString(),
                v => new Uri(v))
            .IsRequired();

        builder
            .Property(x => x.ShopKey)
            .HasConversion<int>()
            .IsRequired();
        
        builder
            .Property(x => x.IsAvailable)
            .IsRequired();
        
        builder
            .Property(x => x.CurrencyKey)
            .HasConversion<int>()
            .IsRequired();

        builder
            .Property(x => x.IsDeleted)
            .IsRequired();
            
        builder
            .HasOne(i => i.User)
            .WithMany(i => i.Items)
            .HasForeignKey(i => i.UserId)
            .IsRequired();
        
        builder
            .HasMany(x => x.PriceChanges)
            .WithOne()
            .HasForeignKey(x => x.ItemId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        builder
            .HasMany(x => x.ParseErrors)
            .WithOne()
            .HasForeignKey(x => x.ItemId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}