using AdessoWorldLeague.Domain.Common;
using AdessoWorldLeague.Domain.Common.Errors;
using AdessoWorldLeague.Domain.Models.Countries;
using AdessoWorldLeague.Domain.Models.Teams;
using FluentResults;

namespace AdessoWorldLeague.Domain.Models.DrawResults;

public class DrawResult : AggregateRoot<DrawResultId>
{
    private List<Group> _groups = [];
    private DrawResult() { }
    public Ruler Ruler { get; set; }
    public DrawResultDate Date { get; set; }
    public IReadOnlyList<Group> Groups => _groups.AsReadOnly();


    public static Result<DrawResult> Create(Ruler ruler, DrawResultDate date, IEnumerable<Team> teams, int numberOfGroups)
    {
        var drawResult = new DrawResult
        {
            Id = new DrawResultId(Guid.NewGuid()),
            Ruler = ruler,
            Date = date
        };
        var createGroupResult = drawResult.CreateGroups(numberOfGroups);
        if (createGroupResult.IsFailed) return createGroupResult;
        var fillResult = drawResult.FillGroupsWithTeams(teams);
        if (fillResult.IsFailed) return fillResult;
        return drawResult;
    }


    private Result CreateGroups(int numberOfGroups)
    {
        const string GroupNames = "ABCDEFGH";

        if (numberOfGroups is not (4 or 8)) return new Error(DrawResultDomainErrors.DrawResultInvalidNumberOfGroups);

        _groups = Enumerable.Range(0, numberOfGroups).Select(i => new Group(
                new GroupId(Guid.NewGuid()),
                new GroupName(GroupNames[i].ToString()
                )
            )
        ).ToList();

        return Result.Ok();
    }

    private Result FillGroupsWithTeams(IEnumerable<Team> teams)
    {
        List<Team> localTeams = ShuffleTeams(teams.ToList());
        while (localTeams.Count != 0)
        {
            foreach (var group in _groups)
            {
                List<Team> filteredTeams = localTeams.Where(m => !group.Teams.Select(n => n.CountryId).Contains(m.CountryId)).ToList();
                if (filteredTeams.Count == 0) continue;
                var team = filteredTeams.First();
                group.Teams.Add(team);
                localTeams.Remove(team);
            }
        }

        return Result.Ok();
    }

    private List<Team> ShuffleTeams(List<Team> teams)
    {
        var random = new Random();
        var count = teams.Count();
        var last = count - 1;
        for (var i = 0; i < last; i++)
        {
            var r = random.Next(i, count);
            (teams[i], teams[r]) = (teams[r], teams[i]);
        }

        return teams;
    }
}