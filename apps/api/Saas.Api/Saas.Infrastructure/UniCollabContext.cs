using Microsoft.EntityFrameworkCore;
using Saas.Domain;

namespace Saas.Infrastructure;

internal class UniCollabContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public UniCollabContext(DbContextOptions<UniCollabContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var userBuilder = modelBuilder.Entity<User>();

        userBuilder.HasKey(c => c.Id);

        userBuilder
            .HasMany(u => u.Friends)
            .WithMany();

        userBuilder.Property(u => u.Username)
            .HasColumnType("nvarchar(32)");
        userBuilder.Property(u => u.Password)
            .HasColumnType("nvarchar(128)");
    }
}