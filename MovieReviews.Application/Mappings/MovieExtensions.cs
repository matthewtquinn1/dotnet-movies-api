using MovieReviews.Domain.Entities;
using MovieReviews.Domain.Models;

namespace MovieReviews.Application.Mappings;

public static class MovieExtensions
{
    public static MovieDto ToDto(this Movie movie)
    {
        return new MovieDto(
            movie.Id,
            movie.Title,
            movie.Description,
            movie.Rating);
    }
}
