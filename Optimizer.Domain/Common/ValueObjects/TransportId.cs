using Optimizer.Domain.Common.Models;

namespace Optimizer.Domain.Common.ValueObjects;

public class TransportId : ValueObject
{
    public Guid Value { get; }

    protected TransportId(Guid value)
    {
        Value = value;
    }

    public static TransportId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static TransportId Create(Guid value)
    {
        return new(value);
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}