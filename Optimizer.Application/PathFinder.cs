using Optimizer.Domain.Common.Entities;
using Optimizer.Domain.Common.ValueObjects;
using Optimizer.Domain.Route.ValueObjects;

namespace Optimizer.Application;

public class PathFinder
{
    public static List<Dictionary<Transport<TransportId>, Station>> FindPath<TTransportId>(
        Station firstStation,
        Station lastStation,
        List<Transport<TTransportId>> allTransport,
        DateTime currentDateTime)
        where TTransportId : TransportId
    {
        var firstStationTransport = new List<Transport<TTransportId>>();
        foreach (var transport in allTransport)
        {
            if (transport.TransportRoutes.Any(r =>
                    r.ArrivalTimes.Any(a => a.Station == firstStation && a.Time > currentDateTime)
                    && r.ArrivalTimes[^1].Station != firstStation))
            {
                firstStationTransport.Add(transport);
            }
        }

        var possibleWays =
            new List<(List<(Transport<TTransportId>, List<ArrivalTime>)> transportRoutes, TimeSpan TotalTime)>();
        var intermediateWays =
            new List<(List<(Transport<TTransportId>, List<ArrivalTime>)> transportRoutes, TimeSpan TotalTime)>();

        foreach (var transport in firstStationTransport)
        {
            foreach (var route in transport.TransportRoutes)
            {
                if (!route.ArrivalTimes.Any(a => a.Station == firstStation && a.Time > currentDateTime)
                    || route.ArrivalTimes[^1].Station == firstStation)
                {
                    continue;
                }

                var firstStationIndex = route.ArrivalTimes
                    .ToList()
                    .FindIndex(a => a.Station == firstStation);

                var lastStationIndex = route.ArrivalTimes
                    .ToList()
                    .FindIndex(a => a.Station == lastStation);

                if (route.ArrivalTimes.Any(a => a.Station == lastStation) && firstStationIndex < lastStationIndex)
                {
                    var stations = new List<ArrivalTime>();
                    for (var i = firstStationIndex; i <= lastStationIndex; i++)
                    {
                        stations.Add(route.ArrivalTimes[i]);
                    }

                    var totalTime = stations[^1].Time - stations[0].Time;
                    var transportRoute = new List<(Transport<TTransportId>, List<ArrivalTime>)>
                    {
                        (transport, stations)
                    };

                    possibleWays.Add((transportRoute, totalTime));
                }

                foreach (var arrivalTime in route.ArrivalTimes.Where(a => a.Time > currentDateTime))
                {
                    //TODO вынести в метод

                    var intermediateStationIndex = route.ArrivalTimes
                        .ToList()
                        .FindIndex(a => a.Station == arrivalTime.Station);

                    if (firstStationIndex < intermediateStationIndex && intermediateStationIndex != lastStationIndex)
                    {
                        var stations = new List<ArrivalTime>();
                        for (var i = firstStationIndex; i <= intermediateStationIndex; i++)
                        {
                            stations.Add(route.ArrivalTimes[i]);
                        }

                        var totalTime = stations[^1].Time - stations[0].Time;
                        var transportRoute = new List<(Transport<TTransportId>, List<ArrivalTime>)>
                        {
                            (transport, stations)
                        };

                        intermediateWays.Add((transportRoute, totalTime));
                    }
                }
            }
        }

        return null;
    }
}