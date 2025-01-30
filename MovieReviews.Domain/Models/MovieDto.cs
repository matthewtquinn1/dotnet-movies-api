namespace MovieReviews.Domain.Models;

public record MovieDto(
    Guid Id,
    string Title,
    string Description,
    double Rating
);
