using Optimizer.Domain.Common.Models;
using Optimizer.Domain.Common.ValueObjects;

namespace Optimizer.Domain.Common.Entities;

public class Transport<TId> : Entity<TId> 
    where TId : TransportId
{
    public int MaxPassengersCount { get; private set; }
    public int PassengersCount { get; private set; }
    
    public Transport(TId id, int maxPassengersCount, int passengersCount) : base(id)
    {
        MaxPassengersCount = maxPassengersCount;
        PassengersCount = passengersCount;
    }
}