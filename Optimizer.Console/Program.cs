using Optimizer.Domain.Bus;
using Optimizer.Domain.Bus.Entities;
using Optimizer.Domain.Bus.ValueObjects;
using Optimizer.Domain.Route;
using Optimizer.Domain.Route.ValueObjects;

var bus = new Bus(BusId.CreateUnique(), 100, new PlateNumber("asd"));
var busStation = new BusStation(BusStationId.CreateUnique(), "Station1");

var arrivalTimes = new List<ArrivalTime<BusStationId>>();
arrivalTimes.Add(new ArrivalTime<BusStationId>(busStation, DateTime.UtcNow));
var route = new Route<Bus, BusId, BusStationId>(RouteId.CreateUnique(), bus, arrivalTimes);
bus.AddRoute(route);

// Можно смело стирать, просто пример использования классов

Console.WriteLine("Hell0");