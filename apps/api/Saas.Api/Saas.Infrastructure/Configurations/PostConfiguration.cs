using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saas.Domain;
using Saas.Domain.Posts;

namespace Saas.Infrastructure.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder
            .HasMany(x => x.Subjects)
            .WithMany();

        builder.Property(x => x.Content)
            .HasColumnType("text");

        builder.Property(x => x.Title)
            .HasColumnType("nvarchar(30)");
    }
}