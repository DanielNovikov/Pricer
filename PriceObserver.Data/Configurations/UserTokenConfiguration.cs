using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceObserver.Data.Models;

namespace PriceObserver.Data.Configurations
{
    public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Token)
                .IsRequired();

            builder
                .Property(x => x.Expired)
                .IsRequired();

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.Tokens)
                .HasForeignKey(x => x.UserId)
                .IsRequired();
        }
    }
}