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

        modelBuilder.Entity<User>().HasMany<Posts>(u => u.Posts).WithOne(t => t.User).HasForeignKey(u => u.UserId);


        modelBuilder.Entity<Posts>(entity =>
        {
            entity.HasKey(e => e.PostId);
            entity.Property(e => e.Body).IsRequired();
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.UserId).IsRequired();
            //entity.Properstyle(e => e.CreatedOn).HasDefaultValue("datetime()");
        });
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.Email).IsRequired();
            entity.HasIndex(x => x.Email).IsUnique();
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.FirstName);
            entity.Property(e => e.LastName);      
            entity.Property(e => e.State);                            
        });

    }
}