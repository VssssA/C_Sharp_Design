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
                    var transportRoutes = new List<(Transport<TTransportId>, List<ArrivalTime>)>
                    {
                        (transport, stations)
                    };

                    possibleWays.Add((transportRoutes, totalTime));
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
                        var transportRoutes = new List<(Transport<TTransportId>, List<ArrivalTime>)>
                        {
                            (transport, stations)
                        };

                        intermediateWays.Add((transportRoutes, totalTime));
                    }
                }
            }
        }

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


                foreach (var transport in startArrivalStationTransport)
                {
                    foreach (var route in transport.TransportRoutes)
                    {
                        if (!route.ArrivalTimes.Any(
                                a => a.Station == startArrival.Station && a.Time > startArrival.Time)
                            || route.ArrivalTimes[^1].Station == startArrival.Station)
                        {
                            continue;
                        }

                        var firstStationIndex = route.ArrivalTimes
                            .ToList()
                            .FindIndex(a => a.Station == startArrival.Station);

                        var lastStationIndex = route.ArrivalTimes
                            .ToList()
                            .FindIndex(a => a.Station == lastStation);

                        if (route.ArrivalTimes.Any(a => a.Station == lastStation) &&
                            firstStationIndex < lastStationIndex)
                        {
                            var stations = new List<ArrivalTime>();
                            for (var i = firstStationIndex; i <= lastStationIndex; i++)
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

                            possibleWays.Add((transportRoutes, totalTime + path.TotalTime));
                        }

                        foreach (var arrivalTime in route.ArrivalTimes.Where(a => a.Time > startArrival.Time))
                        {
                            //TODO вынести в метод

                            var intermediateStationIndex = route.ArrivalTimes
                                .ToList()
                                .FindIndex(a => a.Station == arrivalTime.Station);

                            if (firstStationIndex < intermediateStationIndex &&
                                intermediateStationIndex != lastStationIndex)
                            {
                                var stations = new List<ArrivalTime>();
                                for (var i = firstStationIndex; i <= intermediateStationIndex; i++)
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

                                intermediateWays.Add((transportRoutes, totalTime + path.TotalTime));
                            }
                        }
                    }
                }
            }
        }


        var routesWithAvgMaxPassengersPercent = possibleWays.Select(way =>
            (way, way.transportRoutes
                .Select(r => r.Item2
                    .Average(at => (double)at.PassengersCount / r.Item1.MaxPassengersCount))
                .Average()));

        var transportRoute = routesWithAvgMaxPassengersPercent.MinBy(r =>
            r.way.TotalTime.TotalMinutes * (r.Item2 * r.Item2));
        var transportWay = transportRoute.way;
        var routes = transportWay.transportRoutes;

        var result = new List<(Transport<TTransportId>, List<Station>)>();
        foreach (var way in routes)
        {
            var stations = way.Item2.Select(at => at.Station).ToList();
            result.Add((way.Item1, stations));
        }

        return (result, transportWay.TotalTime, transportRoute.Item2);
    }
}