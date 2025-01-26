using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Saas.Domain;
using Saas.Domain.Posts;
using Saas.Infrastructure.Configurations;

namespace Saas.Infrastructure;

internal class UniCollabContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<ChatRoom> ChatRooms { get; set; }
    
    public UniCollabContext(DbContextOptions<UniCollabContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new ChatRoomConfiguration());
        modelBuilder.ApplyConfiguration(new MessageConfiguration());
    }
}

