using IReckonU.Domain.Entities;
using IReckonU.Domain.Models;

namespace IReckonU.Application.Mappings;

public static class MovieExtensions
{
    public static MovieDto ToDto(this Movie movie)
    {
        return new MovieDto(
            movie.Id,
            movie.Title,
            movie.Description);
    }
}
