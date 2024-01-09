using Optimizer.Domain.Common.Models;

namespace Optimizer.Domain.Common.Entities;

public class Station<TId> : Entity<TId>
    where TId : notnull
{
    public string StationName { get; private set; }
    
    public Station(TId id, string stationName) : base(id)
    {
        StationName = stationName;
    }
}