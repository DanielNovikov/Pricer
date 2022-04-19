using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Data.Persistent.Configurations;

public class ItemPriceChangeConfiguration : IEntityTypeConfiguration<ItemPriceChange>
{
    public void Configure(EntityTypeBuilder<ItemPriceChange> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Created)
            .IsRequired();
            
        builder
            .Property(x => x.OldPrice)
            .IsRequired();
            
        builder
            .Property(x => x.NewPrice)
            .IsRequired();
    }
}