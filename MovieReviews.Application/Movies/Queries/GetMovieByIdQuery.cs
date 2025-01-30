using MovieReviews.Application.Mappings;
using MovieReviews.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MovieReviews.Application.Movies.Queries;

public record GetMovieByIdQuery(Guid Id) : IRequest<MovieDto?>;

public sealed class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, MovieDto?>
{
    private readonly IApplicationContext _context;

    public GetMovieByIdQueryHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<MovieDto?> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var movie = await _context.Movies
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return movie?.ToDto();
    }
}
