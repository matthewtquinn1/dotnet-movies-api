using IReckonU.Domain.Entities;

namespace IReckonU.Domain.Models;

public record MovieDto(
    Guid Id,
    string Title,
    string Description
);
