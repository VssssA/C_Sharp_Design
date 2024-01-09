using Optimizer.Domain.Common.Models;

namespace Optimizer.Domain.Bus.ValueObjects;

public class BusStationId : ValueObject
{
    public Guid Value { get; }

    protected BusStationId(Guid value)
    {
        Value = value;
    }

    public static BusStationId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static BusStationId Create(Guid value)
    {
        return new(value);
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}