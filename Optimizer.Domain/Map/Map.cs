using Optimizer.Domain.Common.Models;
using Optimizer.Domain.Map.ValueObjects;

namespace Optimizer.Domain.Map;

public class Map : Entity<MapId>
{
    public Map(MapId id) : base(id)
    {
    }
}