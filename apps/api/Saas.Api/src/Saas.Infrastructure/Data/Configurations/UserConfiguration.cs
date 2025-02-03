using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saas.Domain;

namespace Saas.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder
            .HasMany(u => u.Friends)
            .WithMany();

        builder
            .HasMany(u => u.Posts)
            .WithOne(p => p.Author);

        builder.Property(u => u.Username)
            .HasColumnType($"varchar({User.MaxUsernameLength})");
        builder.Property(u => u.Password)
            .HasColumnType($"varchar({User.MaxPasswordLength})");
    }
}