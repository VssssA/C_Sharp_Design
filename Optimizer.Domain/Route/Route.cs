using Optimizer.Domain.Common.Entities;
using Optimizer.Domain.Common.Models;
using Optimizer.Domain.Common.ValueObjects;
using Optimizer.Domain.Route.ValueObjects;

namespace Optimizer.Domain.Route;

public class Route<TTransport, TTransportId, TStationId> : Entity<RouteId>
    where TTransport : Transport<TTransportId>
    where TTransportId : TransportId
    where TStationId : notnull
{
    public TTransport Transport { get; private set; }
    private readonly List<ArrivalTime<TStationId>> _arrivalTimes;
    public IReadOnlyList<ArrivalTime<TStationId>> ArrivalTimes => _arrivalTimes;
    
    public Route(RouteId id, TTransport transport, List<ArrivalTime<TStationId>> arrivalTimes) : base(id)
    {
        Transport = transport;
        _arrivalTimes = arrivalTimes;
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