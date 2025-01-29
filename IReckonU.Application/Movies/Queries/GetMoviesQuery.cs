using IReckonU.Application.Mappings;
using IReckonU.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IReckonU.Application.Movies.Queries;

public record GetMoviesQuery() : IRequest<IEnumerable<MovieDto>>;

public sealed class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, IEnumerable<MovieDto>>
{
    private readonly IApplicationContext _context;

    public GetMoviesQueryHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MovieDto>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
    {
        var movies = await _context.Movies
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return movies.Select(movie => movie.ToDto());
    }
}
