using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saas.Domain;

namespace Saas.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(c => c.Id);

        builder
            .HasMany(u => u.Friends)
            .WithMany();

        builder
            .HasMany(u => u.Posts)
            .WithOne(p => p.Author);

        builder.Property(u => u.Username)
            .HasColumnType("nvarchar(32)");
        builder.Property(u => u.Password)
            .HasColumnType("nvarchar(128)");
    }
}