using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceObserver.Data.Models;

namespace PriceObserver.Data.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Text)
                .IsRequired();

            builder
                .Property(x => x.Planned)
                .IsRequired();

            builder
                .Property(x => x.Executed)
                .IsRequired();
        }
    }
}