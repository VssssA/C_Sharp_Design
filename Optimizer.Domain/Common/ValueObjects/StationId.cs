using Optimizer.Domain.Common.Models;

namespace Optimizer.Domain.Common.ValueObjects;

public abstract class StationId : ValueObject
{
    public Guid Value { get; }

    protected StationId(Guid value)
    {
        Value = value;
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}