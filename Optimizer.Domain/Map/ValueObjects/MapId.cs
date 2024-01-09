using Optimizer.Domain.Common.Models;

namespace Optimizer.Domain.Map.ValueObjects;

public sealed class MapId : ValueObject
{
    public Guid Value { get; }

    private MapId(Guid value)
    {
        Value = value;
    }

    public static MapId CreateUnique()
    {
        return new MapId(Guid.NewGuid());
    }

    public static MapId Create(Guid value)
    {
        return new MapId(value);
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}