using Optimizer.Domain.Common.ValueObjects;

namespace Optimizer.Domain.Bus.ValueObjects;

public class BusId : TransportId
{
    private BusId(Guid value) : base(value)
    {
    }
}