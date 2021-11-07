using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceObserver.Model.Data;

namespace PriceObserver.Data.Configurations
{
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
                .HasOne(i => i.User)
                .WithMany(i => i.Items)
                .HasForeignKey(i => i.UserId)
                .IsRequired();

            builder
                .HasOne(x => x.Shop)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.ShopId)
                .IsRequired();
        }
    }
}