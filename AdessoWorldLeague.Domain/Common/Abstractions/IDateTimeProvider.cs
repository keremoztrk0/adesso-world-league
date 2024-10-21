namespace AdessoWorldLeague.Domain.Common.Abstractions;

public interface IDateTimeProvider
{
    public DateTime Now { get; }
}