using AdessoWorldLeague.Domain.Common.Errors;
using AdessoWorldLeague.Domain.Models.Countries;
using AdessoWorldLeague.Domain.Models.DrawResults;
using AdessoWorldLeague.Domain.Models.Teams;
using FluentAssertions;
using FluentResults;
using Xunit;

namespace AdessoWorldLeague.Domain.Test.DrawResultTests;

public class DrawResultTest
{

    [Theory]
    [MemberData(nameof(GetTeamsData))]
    public void DrawResult_Should_Succeed_For_4_Group(List<Team> teams)
    {
        // Arrange
        Ruler ruler = new Ruler("TestName", "TestSurname");
        //TODO: IDateTimeProvider need to be implemented
        DrawResultDate drawResultDate = new DrawResultDate(DateTime.Now);

        int groupCount = 4;

        // Act
        Result<DrawResult> createResult = DrawResult.Create(ruler, drawResultDate, teams, groupCount);
        DrawResult? drawResult = createResult.Value;

        // Assert

        createResult.IsSuccess.Should().BeTrue();
        drawResult.Date.Should().Be(drawResultDate);
        drawResult.Ruler.Should().Be(ruler);
        drawResult.Groups.Should().NotBeNullOrEmpty();
        drawResult.Groups.Count.Should().Be(4);
        //Each group should have different country
        drawResult.Groups.Any(g => g.Teams.GroupBy(m => m.CountryId).Count() == 8).Should().BeTrue();
        drawResult.Groups.All(g => g.Teams.Count == teams.Count / groupCount).Should().BeTrue();
        //Group names should be A, B, C, D
        drawResult.Groups.Select(g => g.Name.Value).Should().BeEquivalentTo(["A", "B", "C", "D"]);
    }

    [Theory]
    [MemberData(nameof(GetTeamsData))]
    public void DrawResult_Should_Succeed_For_8_Group(IEnumerable<Team> teams)
    {
        // Arrange
        Ruler ruler = new Ruler("TestName", "TestSurname");
        //TODO: IDateTimeProvider need to be implemented
        DrawResultDate drawResultDate = new DrawResultDate(DateTime.Now);
        int groupCount = 8;


        // Act
        Result<DrawResult> createResult = DrawResult.Create(ruler, drawResultDate, teams, groupCount);
        DrawResult? drawResult = createResult.Value;
        if (createResult.IsFailed)
        {
            return;
        }
        // Assert
        createResult.IsSuccess.Should().BeTrue();
        drawResult.Date.Should().Be(drawResultDate);
        drawResult.Ruler.Should().Be(ruler);
        drawResult.Groups.Should().NotBeNullOrEmpty();
        drawResult.Groups.Count.Should().Be(8);
        drawResult.Groups.Any(g => g.Teams.GroupBy(m => m.CountryId).Count() == 4).Should().BeTrue();
        drawResult.Groups.All(g => g.Teams.Count == teams.Count() / groupCount).Should().BeTrue();
        drawResult.Groups.Select(g => g.Name.Value).Should().BeEquivalentTo(["A", "B", "C", "D", "E", "F", "G", "H"]);
    }

    [Fact]
    public void DrawResult_Should_Throw_Exception_For_Invalid_Group_Count()
    {
        // Arrange
        Ruler ruler = new Ruler("TestName", "TestSurname");
        DrawResultDate drawResultDate = new DrawResultDate(DateTime.Now);
        //TODO: IDateTimeProvider need to be implemented
        int groupCount = 5;

        // Act
        Result<DrawResult> act = DrawResult.Create(ruler, drawResultDate, [], groupCount);

        // Assert
        act.IsFailed.Should().BeTrue();
        act.Errors.Should().Contain(m => m.Message == DrawResultDomainErrors.DrawResultInvalidNumberOfGroups);
    }

    public static IEnumerable<object[]> GetTeamsData()
    {
        yield return new object[] { GetTeams() };
    }

    private static List<Team> GetTeams()
    {
        CountryId turkeyCountryId = new CountryId(Guid.NewGuid());
        CountryId germanyCountryId = new CountryId(Guid.NewGuid());
        CountryId franceCountryId = new CountryId(Guid.NewGuid());
        CountryId netherlandsCountryId = new CountryId(Guid.NewGuid());
        CountryId portugalCountryId = new CountryId(Guid.NewGuid());
        CountryId italyCountryId = new CountryId(Guid.NewGuid());
        CountryId spainCountryId = new CountryId(Guid.NewGuid());
        CountryId belgiumCountryId = new CountryId(Guid.NewGuid());
        return
        [
            new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Istanbul"), turkeyCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Ankara"), turkeyCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Antalya"), turkeyCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso İzmir"), turkeyCountryId),
            new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Berlin"), germanyCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Frankfurth"), germanyCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Munich"), germanyCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Dortmund"), germanyCountryId),
            new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Paris"), franceCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Lyon"), franceCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Marseille"), franceCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Nice"), franceCountryId),
            new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Amsterdam"), netherlandsCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Rotterdam"), netherlandsCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Lahey"), netherlandsCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Eindhoven"), netherlandsCountryId),
            new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Lisbon"), portugalCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Porto"), portugalCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Braga"), portugalCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Coimbra"), portugalCountryId),
            new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Rome"), italyCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Milano"), italyCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Venice"), italyCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Napoli"), italyCountryId),
            new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Madrid"), spainCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Barcelona"), spainCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Granada"), spainCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Sevilla"), spainCountryId),
            new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Brussels"), belgiumCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Anvers"), belgiumCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Ghent"), belgiumCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Brugge"), belgiumCountryId)
        ];
    }
}