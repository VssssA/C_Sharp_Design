using Optimizer.Domain.Common.Models;
using Optimizer.Domain.Map.ValueObjects;

namespace Optimizer.Domain.Map;

public sealed class Map : Entity<MapId> 
{
    private readonly HashSet<Road> _roads = new();
    public IReadOnlySet<Road> Roads => _roads;
    
    private Map(MapId id) : base(id)
    {
    }

    public static Map Create()
    {
        return new Map(MapId.CreateUnique());
    }
}