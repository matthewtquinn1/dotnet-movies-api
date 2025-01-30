using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace MovieReviews.Infrastructure.Persistance;

/// <summary>
/// This is used by EF Core Migrations when it wishes to run Migrations from somewhere other than the project with a program.cs.
/// </summary>
public sealed class MovieReviewsDbContextFactory : IDesignTimeDbContextFactory<MovieReviewsDbContext>
{
    public MovieReviewsDbContext CreateDbContext(string[] args)
    {
        var pathForProjectWithConfig = Path.Combine(Directory.GetCurrentDirectory(), @"..\MovieReviews.Api");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(pathForProjectWithConfig)
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile($"appsettings.Development.json", optional: true)
            .Build();

        var connectionString = configuration.GetConnectionString("MoviesDb");

        var postgresBuilder = new NpgsqlDataSourceBuilder(connectionString);
        var optionsBuilder = new DbContextOptionsBuilder<MovieReviewsDbContext>();
        optionsBuilder.UseNpgsql(postgresBuilder.Build());

        return new MovieReviewsDbContext(optionsBuilder.Options);
    }
}