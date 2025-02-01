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
        builder.Property(g => g.Id).ValueGeneratedNever();
        
        builder
            .HasMany(g => g.Members)
            .WithMany();

        builder
            .HasOne(g => g.Creator)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasTitle(g => g.Name);

        builder.Navigation(g => g.Creator).IsRequired(false);
    }
}