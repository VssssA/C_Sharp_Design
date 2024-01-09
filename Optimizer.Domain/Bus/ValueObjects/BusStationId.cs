using Optimizer.Domain.Common.ValueObjects;

namespace Optimizer.Domain.Bus.ValueObjects;

public sealed class BusStationId : StationId
{
    private BusStationId(Guid value) : base(value)
    {
    }

    public static BusStationId CreateUnique()
    {
        return new BusStationId(Guid.NewGuid());
    }

    public static BusStationId Create(Guid value)
    {
        return new BusStationId(value);
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}