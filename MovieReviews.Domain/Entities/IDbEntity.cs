namespace MovieReviews.Domain.Entities;

public abstract class DbEntity
{
    public Guid Id { get; set; }
    public int DbId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
