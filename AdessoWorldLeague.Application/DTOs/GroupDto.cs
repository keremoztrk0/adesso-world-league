namespace AdessoWorldLeague.Application.DTOs;

public record GroupDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public List<TeamDto> Teams { get; init; }
}