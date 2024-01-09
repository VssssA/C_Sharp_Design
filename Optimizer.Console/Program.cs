using Optimizer.Domain.Bus;
using Optimizer.Domain.Bus.Entities;
using Optimizer.Domain.Bus.ValueObjects;
using Optimizer.Domain.Route;
using Optimizer.Domain.Route.ValueObjects;

var bus = Bus.Create(100, PlateNumber.Create("asd"));
var busStation = BusStation.Create("Station1");

var arrivalTimes = new List<ArrivalTime> { ArrivalTime.Create(busStation, DateTime.UtcNow) };
var route = Route<Bus, BusId>.Create(bus, arrivalTimes);
bus.AddRoute(route);

// Можно смело стирать, просто пример использования классов

Console.WriteLine("Hell0");