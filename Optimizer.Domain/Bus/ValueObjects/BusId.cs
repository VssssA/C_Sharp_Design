using Optimizer.Domain.Common.ValueObjects;

namespace Optimizer.Domain.Bus.ValueObjects;

public sealed class BusId : TransportId
{
    private BusId(Guid value) : base(value)
    {
    }
    
    public static BusId CreateUnique()
    {
        return new BusId(Guid.NewGuid());
    }

    public static BusId Create(Guid value)
    {
        return new BusId(value);
    }
}