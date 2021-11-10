using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceObserver.Model.Data;

namespace PriceObserver.Data.Configurations
{
    public class ShopConfiguration : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(x => x.Type)
                .HasConversion<int>()
                .IsRequired();
            
            builder
                .Property(x => x.Host)
                .HasMaxLength(150)
                .IsRequired();
            
            builder
                .Property(i => i.LogoUrl)
                .HasConversion(
                    v => v.ToString(),
                    v => new Uri(v))
                .IsRequired();
        }
    }
}