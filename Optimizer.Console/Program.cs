﻿using Optimizer.Application;
using Optimizer.Domain.Bus;
using Optimizer.Domain.Bus.Entities;
using Optimizer.Domain.Bus.ValueObjects;
using Optimizer.Domain.Common.Entities;
using Optimizer.Domain.Route;
using Optimizer.Domain.Route.ValueObjects;
using Optimizer.PathMaker.RouteMaker;
using Optimizer.PathMaker.MapMaker;
using System.Net.Http.Headers;
using Optimizer.Domain.Common.Entities;
using System.Runtime.ExceptionServices;
using System.Security.AccessControl;

/*var bus = Bus.Create(100, PlateNumber.Create("asd"));*/

// var arrivalTimes = new List<ArrivalTime> { ArrivalTime.Create(busStation, DateTime.UtcNow) };
// var route = Route<Bus, BusId>.Create(bus, arrivalTimes);
// bus.AddRoute(route);

internal class Program
{
    private static void Main(string[] args)
    {
        var bus = Bus.Create(100, PlateNumber.Create("bus"));
        var bus2 = Bus.Create(100, PlateNumber.Create("bus2"));
        var busStation = BusStation.Create("Station1");

        var busStation2 = BusStation.Create("Station2");
        var busStation3 = BusStation.Create("Station3");
        var busStation4 = BusStation.Create("Station4");
        var busStation5 = BusStation.Create("Station5");
        var busStation6 = BusStation.Create("Station6");

        var arrivalTimes = new List<ArrivalTime>
        {
            ArrivalTime.Create(busStation, DateTime.UtcNow.AddHours(1)),
            ArrivalTime.Create(busStation2, DateTime.UtcNow.AddHours(2)),
            ArrivalTime.Create(busStation5, DateTime.UtcNow.AddHours(3)),
            ArrivalTime.Create(busStation3, DateTime.UtcNow.AddHours(4)),
        };
        var arrivalTimes2 = new List<ArrivalTime>
        {
            ArrivalTime.Create(busStation3, DateTime.UtcNow.AddHours(1)),
            ArrivalTime.Create(busStation5, DateTime.UtcNow.AddHours(2)),
            ArrivalTime.Create(busStation2, DateTime.UtcNow.AddHours(3)),
            ArrivalTime.Create(busStation, DateTime.UtcNow.AddHours(4)),
        };
        var arrivalTimes3 = new List<ArrivalTime>
        {
            ArrivalTime.Create(busStation2, DateTime.UtcNow.AddHours(1)),
            ArrivalTime.Create(busStation3, DateTime.UtcNow.AddHours(2)),
            ArrivalTime.Create(busStation5, DateTime.UtcNow.AddHours(3)),
            ArrivalTime.Create(busStation, DateTime.UtcNow.AddHours(4)),
            ArrivalTime.Create(busStation6, DateTime.UtcNow.AddHours(5)),
        };
        var arrivalTimes4 = new List<ArrivalTime>
        {
            ArrivalTime.Create(busStation6, DateTime.UtcNow.AddHours(1)),
            ArrivalTime.Create(busStation, DateTime.UtcNow.AddHours(2)),
            ArrivalTime.Create(busStation5, DateTime.UtcNow.AddHours(3)),
            ArrivalTime.Create(busStation3, DateTime.UtcNow.AddHours(4)),
            ArrivalTime.Create(busStation2, DateTime.UtcNow.AddHours(5)),
        };
        var route = Route<BusId>.Create(bus, arrivalTimes);
        var route2 = Route<BusId>.Create(bus, arrivalTimes2);
        var route3 = Route<BusId>.Create(bus, arrivalTimes3);
        var route4 = Route<BusId>.Create(bus, arrivalTimes4);
        bus.AddRoute(route);
        bus.AddRoute(route2);
        bus2.AddRoute(route3);
        bus2.AddRoute(route4);

        PathFinder.FindPath(busStation, busStation2, new List<Transport<BusId>> { bus, bus2 }, DateTime.UtcNow);


/*        var route = RouteMaker.MakeNewRoute();
        Console.WriteLine(route.Transport.PlateNumber.Number);
        Console.WriteLine(route.Transport.Id.Value);
        Console.WriteLine(route.ArrivalTimes.Count);
        Console.WriteLine(route.Transport.MaxPassengersCount);
        Console.WriteLine(route.ArrivalTimes);
        foreach (var time in route.ArrivalTimes)
        {
            Console.WriteLine(time.Time);
        }*/

        var routes = RouteMaker.MakeNewRoutes(5);
        for (int i = 0; i < routes.Count; i++)
        {
            Console.WriteLine(routes[i].Transport.PlateNumber.Number);
            Console.WriteLine(RouteMaker.stopsList[i].StationName);
        }
    }
}