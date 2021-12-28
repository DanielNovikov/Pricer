using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceObserver.Data.Models;

namespace PriceObserver.Data.Configurations;

public class AppNotificationConfiguration : IEntityTypeConfiguration<AppNotification>
{
    public void Configure(EntityTypeBuilder<AppNotification> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Text)
            .IsRequired();

        builder
            .Property(x => x.Executed)
            .IsRequired();
    }
}