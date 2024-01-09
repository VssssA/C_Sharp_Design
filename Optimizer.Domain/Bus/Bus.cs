using Optimizer.Domain.Bus.ValueObjects;
using Optimizer.Domain.Common.Entities;
using Optimizer.Domain.Route;

namespace Optimizer.Domain.Bus;

public class Bus : Transport<BusId>
{
    public List<Route<Bus, BusId, BusStationId>> BusRoutes { get; private set; }
    public PlateNumber PlateNumber { get; private set; }
    
    public Bus(
        BusId id,
        List<Route<Bus, BusId, BusStationId>> busRoutes,
        int maxPassengersCount,
        PlateNumber plateNumber) : base(id, maxPassengersCount, 0)
    {
        BusRoutes = busRoutes;
        PlateNumber = plateNumber;
    }
}