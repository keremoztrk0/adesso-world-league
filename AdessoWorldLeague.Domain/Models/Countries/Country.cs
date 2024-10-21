using AdessoWorldLeague.Domain.Common;

namespace AdessoWorldLeague.Domain.Models.Countries;

public sealed class Country : Entity<CountryId>
{
    private Country(){}
    public Country(CountryId id, CountryName name)
    {
        Id = id;
        Name = name;
    }

    public CountryName Name { get; set; }
}