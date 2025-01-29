﻿namespace IReckonU.Domain.Entities;

public sealed class Movie
{
    public Guid Id { get; set; }
    public int DbId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
