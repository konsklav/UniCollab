using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saas.Domain;
using Saas.Infrastructure.Data.Configurations.Extensions;

namespace Saas.Infrastructure.Data.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(g => g.Id);
        builder.Property(g => g.Id).ValueGeneratedOnAdd();
        
        builder
            .HasMany(g => g.Members)
            .WithMany();

        builder
            .HasOne(g => g.Creator)
            .WithMany();

        builder.HasTitle(g => g.Name);
    }
}