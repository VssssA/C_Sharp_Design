using Optimizer.Domain.Common.Models;
using Optimizer.Domain.Common.ValueObjects;

namespace Optimizer.Domain.Common.Entities;

public abstract class Station : Entity<StationId>
{
    public string StationName { get; private set; }
    
    protected Station(StationId id, string stationName) : base(id)
    {
        StationName = stationName;
    }

    public override string ToString()
    {
        return StationName;
    }
}