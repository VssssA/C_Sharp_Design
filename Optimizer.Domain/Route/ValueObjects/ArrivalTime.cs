using Optimizer.Domain.Common.Entities;
using Optimizer.Domain.Common.Models;

namespace Optimizer.Domain.Route.ValueObjects;

public class ArrivalTime<TStationId> : ValueObject 
    where TStationId : notnull
{
    public Station<TStationId> Station { get; }
    public DateTime Time { get; }
    
    public ArrivalTime(Station<TStationId> station, DateTime time)
    {
        Station = station;
        Time = time;
    }
    
    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Station;
        yield return Time;
    }
}