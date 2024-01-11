using Optimizer.Domain.Common.Entities;
using Optimizer.Domain.Common.Models;
using Optimizer.Domain.Common.ValueObjects;
using Optimizer.Domain.Route.ValueObjects;

namespace Optimizer.Domain.Route;

public sealed class Route<TTransportId> : Entity<RouteId>
    where TTransportId : TransportId
{
    public Transport<TTransportId> Transport { get; private set; }
    private readonly List<ArrivalTime> _arrivalTimes;
    public IReadOnlyList<ArrivalTime> ArrivalTimes => _arrivalTimes;
    
    private Route(RouteId id, Transport<TTransportId> transport, List<ArrivalTime> arrivalTimes) : base(id)
    {
        Transport = transport;
        _arrivalTimes = arrivalTimes;
    }
    
    public static Route<TTransportId> Create(Transport<TTransportId> transport, List<ArrivalTime> arrivalTimes)
    {
        return new Route<TTransportId>(RouteId.CreateUnique(), transport, arrivalTimes);
    }
    
    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Transport;
        foreach (var arrivalTime in _arrivalTimes)
        {
            yield return arrivalTime;
        }
    }
}