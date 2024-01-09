using Optimizer.Domain.Common.Models;

namespace Optimizer.Domain.Common.ValueObjects;

public class TransportId : ValueObject
{
    public Guid Value { get; }

    protected TransportId(Guid value)
    {
        Value = value;
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}