using AdessoWorldLeague.Domain.Common.Abstractions;

namespace AdessoWorldLeague.Infrastructure.Providers;

public class DateTimeProvider:IDateTimeProvider
{
    public DateTime Now { get; } = DateTime.Now;
}