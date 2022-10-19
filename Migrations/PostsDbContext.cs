using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Migrations;

public class PostsDbContext : DbContext
{
    public DbSet<Posts> Posts { get; set; }
    public DbSet<User> Users { get; set; }
    public PostsDbContext(DbContextOptions<PostsDbContext> options)
        : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Posts>(entity =>
        {
            entity.HasKey(e => e.PostId);
            entity.Property(e => e.Post).IsRequired();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.Email).IsRequired();
            entity.HasIndex(x => x.Email).IsUnique();
            entity.Property(e => e.Password).IsRequired();
        });

        modelBuilder.Entity<Posts>().HasData(
    new Posts { 
        PostId = 1,
        Post = "My first post!",
    },
    new Posts { 
        PostId = 2,
        Post = "My second post!",
    }
);
    }
}