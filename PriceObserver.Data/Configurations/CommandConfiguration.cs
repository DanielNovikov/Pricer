using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceObserver.Model.Data;

namespace PriceObserver.Data.Configurations
{
    public class CommandConfiguration: IEntityTypeConfiguration<Command>
    {
        public void Configure(EntityTypeBuilder<Command> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Type)
                .HasConversion<int>()
                .IsRequired();

            builder
                .Property(x => x.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .HasOne(x => x.MenuToRedirect)
                .WithMany()
                .HasForeignKey(x => x.MenuToRedirectId)
                .IsRequired(false);
        }
    }
}