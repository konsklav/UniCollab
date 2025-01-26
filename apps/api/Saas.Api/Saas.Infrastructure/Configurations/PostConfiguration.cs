using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saas.Domain;
using Saas.Domain.Posts;
using Saas.Infrastructure.Configurations.Extensions;

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
        
        builder.Navigation(x => x.Subjects).AutoInclude();
        builder.Navigation(x => x.Author).AutoInclude().IsRequired();

        builder.Property(x => x.Content)
            .HasColumnType("text");

        builder.HasTitle(x => x.Title);

        builder.Property(x => x.Slug)
            .HasColumnType($"nvarchar({Title.MaxLength})");
        
        // Configure an index on the Slug property since we search by slug
        builder.HasIndex(x => x.Slug).IsUnique();
        
        // Also an index for CreatedAt to speed up ordering and filtering by date
        builder.HasIndex(x => x.CreatedAt);
    }
}