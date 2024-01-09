using Optimizer.Domain.Common.Models;

namespace Optimizer.Domain.Route.ValueObjects;

public class RouteId : ValueObject
{
    public Guid Value { get; }

    protected RouteId(Guid value)
    {
        Value = value;
    }

    public static RouteId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static RouteId Create(Guid value)
    {
        return new(value);
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}