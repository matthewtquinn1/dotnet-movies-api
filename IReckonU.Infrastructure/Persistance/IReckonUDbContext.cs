using IReckonU.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IReckonU.Infrastructure.Persistance;

public sealed class IReckonUDbContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }

    public IReckonUDbContext(DbContextOptions<IReckonUDbContext> options) : base(options)
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
