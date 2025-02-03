using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saas.Domain;
using Saas.Infrastructure.Data.Configurations.Extensions;

namespace Saas.Infrastructure.Data.Configurations;

public class ChatRoomConfiguration : IEntityTypeConfiguration<ChatRoom>
{
    public void Configure(EntityTypeBuilder<ChatRoom> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedNever();
        
        builder
            .HasMany(c => c.Participants)
            .WithMany();

        builder
            .HasMany(c => c.Messages)
            .WithOne();

        builder.HasTitle(x => x.Name);
    }
}