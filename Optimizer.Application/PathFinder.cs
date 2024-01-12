using Optimizer.Domain.Common.Entities;
using Optimizer.Domain.Common.ValueObjects;
using Optimizer.Domain.Route.ValueObjects;

namespace Optimizer.Application;

public class PathFinder
{
    public static (List<(Transport<TTransportId>, List<Station>)>, TimeSpan Time, double avgMaxPassPercent)
        FindPath<TTransportId>(
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
            new List<(List<(Transport<TTransportId>, List<ArrivalTime>)> TransportRoutes, TimeSpan TotalTime)>();
        var refreshRoutes = false;
        MakeRoutes(lastStation, possibleWays, intermediateWays, firstStation, firstStationTransport, currentDateTime, refreshRoutes);

        var transfersCount = 2;
        for (var j = 0; j < transfersCount; j++)
        {
            var paths = intermediateWays.ToList();
            intermediateWays.Clear();
            foreach (var path in paths)
            {
                var lastTransportRoute = path.TransportRoutes.Last();
                var startArrival = lastTransportRoute.Item2.Last();
                var startArrivalStationTransport = new List<Transport<TTransportId>>();
                foreach (var transport in allTransport)
                {
                    if (transport.TransportRoutes.Any(r =>
                            r.ArrivalTimes.Any(a => a.Station == startArrival.Station && a.Time > startArrival.Time)
                            && r.ArrivalTimes[^1].Station != startArrival.Station))
                    {
                        startArrivalStationTransport.Add(transport);
                    }
                }
                refreshRoutes = true;
                MakeRoutes(lastStation, possibleWays, intermediateWays, startArrival.Station, startArrivalStationTransport,startArrival.Time, refreshRoutes);
            }
        }


        var routesWithAvgMaxPassengersPercent = possibleWays.Select(way =>
            (way, way.transportRoutes
                .Select(r => r.Item2
                    .Average(at => (double)at.PassengersCount / r.Item1.MaxPassengersCount))
                .Average()));

        var transportRoute = routesWithAvgMaxPassengersPercent.MinBy(r =>
            r.way.TotalTime.TotalMinutes * (r.Item2 * r.Item2));
        var (transportRoutes, TotalTime) = transportRoute.way;
        var routes = transportRoutes;

        var result = new List<(Transport<TTransportId>, List<Station>)>();
        foreach (var way in routes)
        {
            var stations = way.Item2.Select(at => at.Station).ToList();
            result.Add((way.Item1, stations));
        }

        return (result, TotalTime, transportRoute.Item2);
    }

    private static void MakeRoutes<TTransportId>(
        Station lastStation,
        List<(List<(Transport<TTransportId>, List<ArrivalTime>)> transportRoutes, TimeSpan TotalTime)> possibleWays,
        List<(List<(Transport<TTransportId>, List<ArrivalTime>)> TransportRoutes, TimeSpan TotalTime)> intermediateWays,
        Station station,
        List<Transport<TTransportId>> startArrivalStationTransport,
        DateTime time,
        bool refresh) where TTransportId : TransportId
    {
        foreach (var transport in startArrivalStationTransport)
        {
            foreach (var route in transport.TransportRoutes)
            {
                if (!route.ArrivalTimes.Any(
                        a => a.Station == station && a.Time > time)
                    || route.ArrivalTimes[^1].Station == station)
                {
                    continue;
                }
                var firstStationIndex = route.ArrivalTimes
                    .ToList()
                    .FindIndex(a => a.Station == station);

                var lastStationIndex = route.ArrivalTimes
                    .ToList()
                    .FindIndex(a => a.Station == lastStation);

                if (route.ArrivalTimes.Any(a => a.Station == lastStation) && firstStationIndex < lastStationIndex)
                {
                    AddOrRefreshRoutesAndTime(possibleWays, transport, route, firstStationIndex, lastStationIndex, refresh);
                }

                foreach (var arrivalTime in route.ArrivalTimes.Where(a => a.Time > time))
                {
                    var intermediateStationIndex = route.ArrivalTimes
                        .ToList()
                        .FindIndex(a => a.Station == arrivalTime.Station);

                    if (firstStationIndex < intermediateStationIndex && intermediateStationIndex != lastStationIndex)
                    {
                        AddOrRefreshRoutesAndTime(intermediateWays, transport, route, firstStationIndex, intermediateStationIndex, refresh);
                    }
                }
            }
        }
    }

    private static void RefreshRoutesAndTime<TTransportId>(
        List<(List<(Transport<TTransportId>, List<ArrivalTime>)> TransportRoutes, TimeSpan TotalTime)> ways,
        (List<(Transport<TTransportId>, List<ArrivalTime>)> TransportRoutes, TimeSpan TotalTime) path,
        Transport<TTransportId> transport,
        Domain.Route.Route<TTransportId> route,
        int firstIndex,
        int secondIndex) where TTransportId : TransportId
    {
        var stations = new List<ArrivalTime>();
        for (var i = firstIndex; i <= secondIndex; i++)
        {
            stations.Add(route.ArrivalTimes[i]);
        }

        var totalTime = stations[^1].Time - stations[0].Time;
        var transportRoutes = new List<(Transport<TTransportId>, List<ArrivalTime>)>();
        foreach (var oldTransportRoute in path.TransportRoutes)
        {
            transportRoutes.Add(oldTransportRoute);
        }

        transportRoutes.Add((transport, stations));

        ways.Add((transportRoutes, totalTime + path.TotalTime));
    }

    private static void AddOrRefreshRoutesAndTime<TTransportId>(
    List<(List<(Transport<TTransportId>, List<ArrivalTime>)> TransportRoutes, TimeSpan TotalTime)> ways,
    Transport<TTransportId> transport,
    Domain.Route.Route<TTransportId> route,
    int firstIndex,
    int secondIndex,
    bool refresh) where TTransportId : TransportId
    {
        var stations = new List<ArrivalTime>();
        for (var i = firstIndex; i <= secondIndex; i++)
        {
            stations.Add(route.ArrivalTimes[i]);
        }

        var totalTime = stations[^1].Time - stations[0].Time;

        if (refresh)
        {
            foreach (var path in ways.ToList())
            {
                var lastStationTime = path.TransportRoutes.Last().Item2.Last().Time;
                if (lastStationTime <= stations[0].Time)
                {
                    RefreshRoutesAndTime(ways, path, transport, route, firstIndex, secondIndex);
                }
            }
        }
        else
        {
            var transportRoutes = new List<(Transport<TTransportId>, List<ArrivalTime>)>
        {
            (transport, stations)
        };

            ways.Add((transportRoutes, totalTime));
        }
    }

}