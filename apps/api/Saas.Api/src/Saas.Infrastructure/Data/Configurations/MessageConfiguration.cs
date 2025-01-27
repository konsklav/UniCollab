using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saas.Domain;

namespace Saas.Infrastructure.Data.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).ValueGeneratedNever();

        builder.HasIndex(m => m.SentAt);
        
        builder
            .HasOne(m => m.Sender)
            .WithMany();
        
        builder.Property(m => m.Content)
            .HasColumnType("nvarchar(500)");
    }
}