using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceObserver.Data.Models;

namespace PriceObserver.Data.Configurations
{
    public class CommandConfiguration : IEntityTypeConfiguration<Command>
    {
        public void Configure(EntityTypeBuilder<Command> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Type)
                .HasConversion<int>()
                .IsRequired();

            builder
                .HasOne(x => x.Resource)
                .WithOne()
                .HasForeignKey<Command>(x => x.ResourceId)
                .IsRequired(false);
            
            builder
                .HasOne(x => x.MenuToRedirect)
                .WithMany()
                .HasForeignKey(x => x.MenuToRedirectId)
                .IsRequired(false);
        }
    }
}