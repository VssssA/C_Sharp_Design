using Optimizer.Domain.Common.Entities;
using Optimizer.Domain.Common.Models;

namespace Optimizer.Domain.Map.ValueObjects;

public sealed class Road : ValueObject
{
    public Station StartStation { get; } 
    public Station EndStation { get; } 
    
    private Road(Station startStation, Station endStation)
    {
        StartStation = startStation;
        EndStation = endStation;
    }

    public static Road Create(Station startStation, Station endStation)
    {
        return new Road(startStation, endStation);
    }
    
    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return StartStation;
        yield return EndStation;
    }
}