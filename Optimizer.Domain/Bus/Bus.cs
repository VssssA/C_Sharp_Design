using Optimizer.Domain.Bus.ValueObjects;
using Optimizer.Domain.Common.Entities;
using Optimizer.Domain.Route;

namespace Optimizer.Domain.Bus;

public sealed class Bus : Transport<BusId>
{
    private readonly List<Route<Bus, BusId>> _busRoutes = new();
    public IReadOnlyList<Route<Bus, BusId>> BusRoutes => _busRoutes;
    public PlateNumber PlateNumber { get; private set; }
    
    private Bus(
        BusId id,
        int maxPassengersCount,
        PlateNumber plateNumber) : base(id, maxPassengersCount, 0)
    {
        PlateNumber = plateNumber;
    }

    public static Bus Create(
        int maxPassengersCount,
        PlateNumber plateNumber)
    {
        return new Bus(BusId.CreateUnique(), maxPassengersCount, plateNumber);
    }

    public void AddRoute(Route<Bus, BusId> route)
    {
        _busRoutes.Add(route);
    }
}