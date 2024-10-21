using AdessoWorldLeague.Domain.Common;
using AdessoWorldLeague.Domain.Models.Countries;

namespace AdessoWorldLeague.Domain.Models.Teams;

public sealed class Team:Entity<TeamId>
{
    private Team(){}
    public Team(TeamId id, TeamName name, CountryId countryId)
    {
        Id = id;
        Name = name;
        CountryId = countryId;
    }
    public TeamName Name { get; set; }
    public CountryId CountryId { get; set; }
    public Country Country { get; set; }
}