using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceObserver.Data.Models;

namespace PriceObserver.Data.Configurations
{
    public class MenuCommandConfiguration: IEntityTypeConfiguration<MenuCommand>
    {
        public void Configure(EntityTypeBuilder<MenuCommand> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasOne(x => x.Menu)
                .WithMany(x => x.MenuCommands)
                .HasForeignKey(x => x.MenuId)
                .IsRequired();

            builder
                .HasOne(x => x.Command)
                .WithMany(x => x.CommandMenus)
                .HasForeignKey(x => x.CommandId)
                .IsRequired();
        }
    }
}