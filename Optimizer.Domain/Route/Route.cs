using Optimizer.Domain.Common.Entities;
using Optimizer.Domain.Common.Models;
using Optimizer.Domain.Common.ValueObjects;
using Optimizer.Domain.Route.ValueObjects;

namespace Optimizer.Domain.Route;

public sealed class Route<TTransport, TTransportId> : Entity<RouteId>
    where TTransport : Transport<TTransportId>
    where TTransportId : TransportId
{
    public TTransport Transport { get; private set; }
    private readonly List<ArrivalTime> _arrivalTimes;
    public IReadOnlyList<ArrivalTime> ArrivalTimes => _arrivalTimes;
    
    private Route(RouteId id, TTransport transport, List<ArrivalTime> arrivalTimes) : base(id)
    {
        Transport = transport;
        _arrivalTimes = arrivalTimes;
    }
    
    public static Route<TTransport, TTransportId> Create(TTransport transport, List<ArrivalTime> arrivalTimes)
    {
        return new Route<TTransport, TTransportId>(RouteId.CreateUnique(), transport, arrivalTimes);
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