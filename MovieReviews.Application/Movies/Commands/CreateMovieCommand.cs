using MovieReviews.Application.Mappings;
using MovieReviews.Domain.Entities;
using MovieReviews.Domain.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MovieReviews.Application.Movies.Commands;

public record CreateMovieCommand(
    [Required][MinLength(1)][MaxLength(225)] string Title,
    [Required][MinLength(1)][MaxLength(225)] string Description,
    [Required][Range(1, 5)] double Rating
) : IRequest<MovieDto>;

public sealed class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, MovieDto>
{
    private readonly IApplicationContext _context;

    public CreateMovieCommandHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<MovieDto> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        // TODO: Implement protection based on combination of Title and perhaps, Director? or some unique identifier.

        var movie = _context.Movies.Add(new Movie
        {
            Title = request.Title,
            Description = request.Description,
            Rating = request.Rating,
        });
        _ = await _context.SaveChangesAsync(cancellationToken);

        return movie.Entity.ToDto();
    }
}
