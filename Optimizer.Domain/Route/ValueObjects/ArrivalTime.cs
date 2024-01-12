using Optimizer.Domain.Common.Entities;
using Optimizer.Domain.Common.Models;

namespace Optimizer.Domain.Route.ValueObjects;

public sealed class ArrivalTime : ValueObject
{
    public Station Station { get; }
    public DateTime Time { get; }
    public int PassengersCount { get; set; }
    
    private ArrivalTime(Station station, DateTime time, int passengersCount)
    {
        Station = station;
        Time = time;
        PassengersCount = passengersCount;
    }

    public static ArrivalTime Create(Station station, DateTime time, int passengersCount)
    {
        return new ArrivalTime(station, time, passengersCount);
    }
    
    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Station;
        yield return Time;
        yield return PassengersCount;
    }

    public override string ToString()
    {
        return $"Station: {Station}, ArrivalTime: {Time:hh:mm:ss}";
    }
}