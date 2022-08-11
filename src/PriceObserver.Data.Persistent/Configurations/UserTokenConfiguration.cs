using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Data.Persistent.Configurations;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Token)
            .IsRequired();

        builder
            .Property(x => x.Expiration)
            .IsRequired();

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Tokens)
            .HasForeignKey(x => x.UserId)
            .IsRequired();
    }
}