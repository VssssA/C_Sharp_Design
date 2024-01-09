using Optimizer.Domain.Common.Models;

namespace Optimizer.Domain.Map.ValueObjects;

public class MapId : ValueObject
{
    public Guid Value { get; }

    protected MapId(Guid value)
    {
        Value = value;
    }

    public static MapId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static MapId Create(Guid value)
    {
        return new(value);
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}