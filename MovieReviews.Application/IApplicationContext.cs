using MovieReviews.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MovieReviews.Application;

public interface IApplicationContext
{
    DbSet<Movie> Movies { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
