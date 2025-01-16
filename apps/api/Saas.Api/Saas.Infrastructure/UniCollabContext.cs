using Microsoft.EntityFrameworkCore;
using Saas.Domain;
using Saas.Infrastructure.Configurations;

namespace Saas.Infrastructure;

internal class UniCollabContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    
    public UniCollabContext(DbContextOptions<UniCollabContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new PostConfiguration());
    }
}