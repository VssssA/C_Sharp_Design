using Optimizer.Domain.Common.Models;
using Optimizer.Domain.Common.ValueObjects;
using Optimizer.Domain.Route;

namespace Optimizer.Domain.Common.Entities;

public abstract class Transport<TId> : Entity<TId>
    where TId : TransportId
{
    public int MaxPassengersCount { get; private set; }
    private readonly List<Route<TId>> _transportRoutes = new();
    public IReadOnlyList<Route<TId>> TransportRoutes => _transportRoutes;

    protected Transport(TId id, int maxPassengersCount) : base(id)
    {
        MaxPassengersCount = maxPassengersCount;
    }

    public void AddRoute(Route<TId> route)
    {
        _transportRoutes.Add(route);
    }
}