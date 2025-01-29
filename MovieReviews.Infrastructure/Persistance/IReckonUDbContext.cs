using MovieReviews.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MovieReviews.Infrastructure.Persistance;

public sealed class MovieReviewsDbContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }

    public MovieReviewsDbContext(DbContextOptions<MovieReviewsDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(entity => 
        {
            entity.ToTable("Movies");

            entity.HasKey(e => e.DbId);

            entity
                .Property(e => e.Id)
                .HasDefaultValueSql("NewId()");

            entity
                .Property(e => e.Title)
                .HasMaxLength(225)
                .IsRequired();

            entity
                .Property(e => e.Description)
                .HasMaxLength(225)
                .IsRequired();

            entity
                .Property(e => e.CreatedAt)
                .HasColumnType("timestamp with time zone")
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity
                .Property(e => e.UpdatedAt)
                .HasColumnType("timestamp with time zone")
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }
}
