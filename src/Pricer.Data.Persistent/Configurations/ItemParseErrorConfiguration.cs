using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pricer.Data.Persistent.Models;

namespace Pricer.Data.Persistent.Configurations;

public class ItemParseErrorConfiguration : IEntityTypeConfiguration<ItemParseResult>
{
    public void Configure(EntityTypeBuilder<ItemParseResult> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.ItemId)
            .IsRequired();

        builder
            .Property(x => x.IsSuccess)
            .IsRequired();
        
        builder
            .Property(x => x.Created)
            .IsRequired();
    }
}