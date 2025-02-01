namespace MovieReviews.Domain.Entities;

public sealed class Movie : DbEntity
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public double Rating { get; set; }
}
