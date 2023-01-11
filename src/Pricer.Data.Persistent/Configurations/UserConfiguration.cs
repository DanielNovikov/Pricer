using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pricer.Data.Persistent.Models;

namespace Pricer.Data.Persistent.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();
        
        builder.HasIndex(u => u.ExternalId);
        
        builder
            .Property(u => u.ExternalId)
            .IsRequired();

        builder
            .Property(u => u.FirstName)
            .HasMaxLength(100)
            .IsRequired(false);
            
        builder
            .Property(u => u.LastName)
            .HasMaxLength(100)
            .IsRequired(false);

        builder
            .Property(u => u.Username)
            .HasMaxLength(250)
            .IsRequired(false);

        builder
            .Property(x => x.IsActive)
            .HasDefaultValueSql("true")
            .IsRequired();

        builder
            .Property(x => x.MenuKey)
            .HasConversion<int>()
            .IsRequired();
        
        builder
            .Property(x => x.SelectedLanguageKey)
            .HasConversion<int>()
            .IsRequired();
    }
}