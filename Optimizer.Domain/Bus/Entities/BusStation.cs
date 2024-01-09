using Optimizer.Domain.Bus.ValueObjects;
using Optimizer.Domain.Common.Entities;
using Optimizer.Domain.Common.ValueObjects;

namespace Optimizer.Domain.Bus.Entities;

public sealed class BusStation : Station
{
    private BusStation(StationId id, string stationName) : base(id, stationName)
    {
    }

    public static BusStation Create(string stationName)
    {
        return new BusStation(BusStationId.CreateUnique(), stationName);
    }
}