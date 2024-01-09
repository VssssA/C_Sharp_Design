using Optimizer.Domain.Common.Entities;
using Optimizer.Domain.Common.Models;

namespace Optimizer.Domain.Route.ValueObjects;

public sealed class ArrivalTime : ValueObject
{
    public Station Station { get; }
    public DateTime Time { get; }
    
    private ArrivalTime(Station station, DateTime time)
    {
        Station = station;
        Time = time;
    }

    public static ArrivalTime Create(Station station, DateTime time)
    {
        return new ArrivalTime(station, time);
    }
    
    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Station;
        yield return Time;
    }
}