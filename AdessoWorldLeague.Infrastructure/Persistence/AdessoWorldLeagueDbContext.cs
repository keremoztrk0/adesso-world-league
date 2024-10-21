using AdessoWorldLeague.Domain.Models;
using AdessoWorldLeague.Domain.Models.Countries;
using AdessoWorldLeague.Domain.Models.DrawResults;
using AdessoWorldLeague.Domain.Models.Teams;
using Microsoft.EntityFrameworkCore;

namespace AdessoWorldLeague.Infrastructure.Persistence;

public sealed class AdessoWorldLeagueDbContext : DbContext
{
    public AdessoWorldLeagueDbContext(DbContextOptions<AdessoWorldLeagueDbContext> options) : base(options) { }

    public DbSet<DrawResult> DrawRecords { get; set; }
    public DbSet<Team> Teams { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DrawResult>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasConversion(m => m.Value, d => new DrawResultId(d));
            entity.OwnsOne(e => e.Ruler);
            entity.Property(e => e.Date).HasConversion(m=>m.Value,d=>new DrawResultDate(d)).IsRequired();
            entity.HasMany(e => e.Groups).WithMany();
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasConversion(m => m.Value, d => new TeamId(d));
            entity.Property(e => e.Name).HasConversion(m=>m.Value,d=>new TeamName(d)).IsRequired();
            entity.HasOne(e => e.Country).WithMany().HasForeignKey(e => e.CountryId);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasConversion(m => m.Value, d => new CountryId(d));

            entity.Property(e => e.Name).HasConversion(m=>m.Value,d=>new CountryName(d)).IsRequired();
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasConversion(m => m.Value, d => new GroupId(d));
            entity.Property(e => e.Name).HasConversion(m => m.Value, d => new GroupName(d)).IsRequired();
            entity.HasMany(e => e.Teams).WithMany();
        });

        var turkeyCountryId = new CountryId(Guid.NewGuid());
        var germanyCountryId = new CountryId(Guid.NewGuid());
        var franceCountryId = new CountryId(Guid.NewGuid());
        var netherlandsCountryId = new CountryId(Guid.NewGuid());
        var portugalCountryId = new CountryId(Guid.NewGuid());
        var italyCountryId = new CountryId(Guid.NewGuid());
        var spainCountryId = new CountryId(Guid.NewGuid());
        var belgiumCountryId = new CountryId(Guid.NewGuid());
        modelBuilder.Entity<Country>().HasData(
            [
                new Country(turkeyCountryId, new CountryName("Turkey")),
                new Country(germanyCountryId, new CountryName("Germany")),
                new Country(franceCountryId, new CountryName("France")),
                new Country(netherlandsCountryId, new CountryName("Netherlands")),
                new Country(portugalCountryId, new CountryName("Portugal")),
                new Country(italyCountryId, new CountryName("Italy")),
                new Country(spainCountryId, new CountryName("Spain")),
                new Country(belgiumCountryId, new CountryName("Belgium"))
            ]
        );
        modelBuilder.Entity<Team>().HasData
        (
            [
                new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Istanbul"), turkeyCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Ankara"), turkeyCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Antalya"), turkeyCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso İzmir"), turkeyCountryId),
                new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Berlin"), germanyCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Frankfurth"), germanyCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Munich"), germanyCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Dortmund"), germanyCountryId),
                new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Paris"), franceCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Lyon"), franceCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Marseille"), franceCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Nice"), franceCountryId),
                new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Amsterdam"), netherlandsCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Rotterdam"), netherlandsCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Lahey"), netherlandsCountryId),
                new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Eindhoven"), netherlandsCountryId),
                new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Lisbon"), portugalCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Porto"), portugalCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Braga"), portugalCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Coimbra"), portugalCountryId),
                new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Rome"), italyCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Milano"), italyCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Venice"), italyCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Napoli"), italyCountryId),
                new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Madrid"), spainCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Barcelona"), spainCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Granada"), spainCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Sevilla"), spainCountryId),
                new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Brussels"), belgiumCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Anvers"), belgiumCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Ghent"), belgiumCountryId), new Team(new TeamId(Guid.NewGuid()), new TeamName("Adesso Brugge"), belgiumCountryId)
            ]
        );
    }
}