using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pricer.Data.Persistent.Models;

namespace Pricer.Data.Persistent.Configurations;

public class AppNotificationConfiguration : IEntityTypeConfiguration<AppNotification>
{
    public void Configure(EntityTypeBuilder<AppNotification> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Content)
            .HasConversion<int>()
            .IsRequired();

        builder
            .Property(x => x.VideoUrl)
            .IsRequired(false);

        builder
            .Property(x => x.Executed)
            .IsRequired();
    }
}