using Optimizer.Domain.Common.Entities;
using Optimizer.Domain.Common.Models;

namespace Optimizer.Domain.Map.ValueObjects;

public class Road<TStationId> : ValueObject 
    where TStationId : notnull
{
    public Station<TStationId> StartStation { get; } 
    public Station<TStationId> EndStation { get; } 
    
    public Road(Station<TStationId> startStation, Station<TStationId> endStation)
    {
        StartStation = startStation;
        EndStation = endStation;
    }
    
    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return StartStation;
        yield return EndStation;
    }
}