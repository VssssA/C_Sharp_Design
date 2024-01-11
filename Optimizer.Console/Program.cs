using Optimizer.Domain.Bus;
using Optimizer.Domain.Bus.Entities;
using Optimizer.Domain.Bus.ValueObjects;
using Optimizer.Domain.Route;
using Optimizer.Domain.Route.ValueObjects;
using Optimizer.PathMaker.RouteMaker;
using Optimizer.PathMaker.MapMaker;
using System.Net.Http.Headers;
using Optimizer.Domain.Common.Entities;
using System.Runtime.ExceptionServices;
using System.Security.AccessControl;

/*var bus = Bus.Create(100, PlateNumber.Create("asd"));
var busStation = BusStation.Create("Station1");
var arrivalTimes = new List<ArrivalTime> { ArrivalTime.Create(busStation, DateTime.UtcNow) };
var route = Route<Bus, BusId>.Create(bus, arrivalTimes);
bus.AddRoute(route);*/

internal class Program
{
    private static void Main(string[] args)
    {


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