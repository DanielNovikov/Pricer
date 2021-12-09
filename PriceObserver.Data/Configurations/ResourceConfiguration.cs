using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceObserver.Data.Models;

namespace PriceObserver.Data.Configurations
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasIndex(x => x.Key)
                .IsUnique();

            builder
                .Property(x => x.Key)
                .IsRequired();

            builder
                .Property(x => x.Value)
                .IsRequired();
        }
    }
}