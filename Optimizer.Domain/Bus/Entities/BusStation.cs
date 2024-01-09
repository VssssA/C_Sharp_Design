using Optimizer.Domain.Bus.ValueObjects;
using Optimizer.Domain.Common.Entities;

namespace Optimizer.Domain.Bus.Entities;

public class BusStation : Station<BusStationId>
{
    public BusStation(BusStationId id, string stationName) : base(id, stationName)
    {
    }
}