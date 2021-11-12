using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceObserver.Model.Data;

namespace PriceObserver.Data.Configurations
{
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

            builder
                .HasOne(x => x.Item)
                .WithMany(x => x.PriceChanges)
                .HasForeignKey(x => x.ItemId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}