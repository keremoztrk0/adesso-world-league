namespace AdessoWorldLeague.Application.DTOs;

public record TeamDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string CountryName { get; init; }
}