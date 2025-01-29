using IReckonU.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IReckonU.Application;

public interface IApplicationContext
{
    DbSet<Movie> Movies { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
