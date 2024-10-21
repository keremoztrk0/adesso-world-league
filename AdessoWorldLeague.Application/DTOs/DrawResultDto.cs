namespace AdessoWorldLeague.Application.DTOs;


public record DrawResultDto
{
    public Guid Id { get; init; }
    public string RulerName { get; init; }
    public string RulerSurname { get; init; }
    public DateTime DrawDate { get; init; }
    public List<GroupDto> Groups { get; init; }
}