using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceObserver.Model.Data;

namespace PriceObserver.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder
                .Property(u => u.Id)
                .ValueGeneratedNever();

            builder
                .Property(u => u.FirstName)
                .HasMaxLength(100)
                .IsRequired();
            
            builder
                .Property(u => u.LastName)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(u => u.Username)
                .HasMaxLength(250)
                .IsRequired();
        }
    }
}