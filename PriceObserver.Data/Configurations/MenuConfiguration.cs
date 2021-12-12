using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceObserver.Data.Models;

namespace PriceObserver.Data.Configurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.ResourceKey)
                .IsRequired();

            builder
                .Property(x => x.Type)
                .HasConversion<int>()
                .IsRequired();

            builder
                .Property(x => x.CanExpectInput)
                .IsRequired();
            
            builder
                .Property(x => x.IsDefault)
                .IsRequired();

            builder
                .HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId);
        }
    }
}