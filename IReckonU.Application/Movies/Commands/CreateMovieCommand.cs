using IReckonU.Application.Mappings;
using IReckonU.Domain.Entities;
using IReckonU.Domain.Models;
using MediatR;

namespace IReckonU.Application.Movies.Commands;

public record CreateMovieCommand(
    string Title,
    string Description
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
        });
        _ = await _context.SaveChangesAsync(cancellationToken);

        return movie.Entity.ToDto();
    }
}
