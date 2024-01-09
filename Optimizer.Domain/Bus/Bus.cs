using Optimizer.Domain.Bus.ValueObjects;
using Optimizer.Domain.Common.Entities;
using Optimizer.Domain.Route;

namespace Optimizer.Domain.Bus;

public class Bus : Transport<BusId>
{
    private readonly List<Route<Bus, BusId, BusStationId>> _busRoutes = new();
    public IReadOnlyList<Route<Bus, BusId, BusStationId>> BusRoutes => _busRoutes;
    public PlateNumber PlateNumber { get; private set; }
    
    public Bus(
        BusId id,
        int maxPassengersCount,
        PlateNumber plateNumber) : base(id, maxPassengersCount, 0)
    {
        PlateNumber = plateNumber;
    }

    public void AddRoute(Route<Bus, BusId, BusStationId> route)
    {
        _busRoutes.Add(route);
    }
}