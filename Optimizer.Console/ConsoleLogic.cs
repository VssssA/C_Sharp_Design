using Optimizer.Application;
using Optimizer.Domain.Bus.Entities;
using Optimizer.Domain.Bus.ValueObjects;
using Optimizer.Domain.Bus;
using Optimizer.Domain.Common.Entities;
using Optimizer.Domain.Route.ValueObjects;
using Optimizer.Domain.Route;
using Optimizer.PathMaker.RouteMaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimizer.Console.ConsoleLogic
{
    internal class ConsoleLogic
    {
        private static readonly List<Transport<BusId>> Buses = new();
        private static readonly List<BusStation> BusStations = new();
        private static readonly List<Route<BusId>> Routes = new();

        public static void Makebus()
        {
            System.Console.WriteLine("Введите максимальное количество пассажиров");
            var maxPassengersCount = int.Parse(System.Console.ReadLine());
            System.Console.WriteLine("Введите знаковый номер автобуса");
            var plateNumber = System.Console.ReadLine();
            Buses.Add(Bus.Create(maxPassengersCount, PlateNumber.Create(plateNumber)));
            System.Console.WriteLine("Вы создали новый автобус под номером {0}", plateNumber);
            System.Console.WriteLine();
        }

        public static void MakeStation()
        {
            System.Console.WriteLine("Введите название остановки");
            var busStationName = System.Console.ReadLine();
            BusStations.Add(BusStation.Create(busStationName));
            System.Console.WriteLine("Вы создали остановку {0}", busStationName);
            System.Console.WriteLine();
        }

        public static void MakeRoute()
        {
            if (Buses.Count < 1)
            {
                System.Console.WriteLine("Пока не создано автобусов для маршрута");
                System.Console.WriteLine();
                return;
            }

            if (BusStations.Count < 2)
            {
                System.Console.WriteLine("Пока не создано как минимум двух станций для маршрута");
                System.Console.WriteLine();
                return;
            }

            System.Console.WriteLine("Выберите автобус для маршрута");
            PrintAllBuses();
            var busNumber = int.Parse(System.Console.ReadLine());
            if (busNumber < 1 || busNumber > Buses.Count)
            {
                System.Console.WriteLine("Неверный индекс автобуса");
                System.Console.WriteLine();
                return;
            }

            System.Console.WriteLine("Сколько станций в маршруте?");
            var busStationsCount = int.Parse(System.Console.ReadLine());
            var arrivalTimes = new List<ArrivalTime>();
            for (var i = 0; i < busStationsCount; i++)
            {
                System.Console.WriteLine("Создание маршрута №{0}", i + 1);
                System.Console.WriteLine("Выберите станцию");
                PrintAllStations();

                var busStationIndex = int.Parse(System.Console.ReadLine());
                var date = DateTime.UtcNow.AddHours(i + 1);


                System.Console.WriteLine("Введите количество людей на остановке");
                var peoplesCount = int.Parse(System.Console.ReadLine());
                arrivalTimes.Add(ArrivalTime.Create(BusStations[busStationIndex - 1], date, peoplesCount));
            }

            var newRoute = Route<BusId>.Create(Buses[busNumber - 1], arrivalTimes);
            Routes.Add(Route<BusId>.Create(Buses[busNumber - 1], arrivalTimes));
            Buses[busNumber - 1].AddRoute(newRoute);
        }

        public static void PrintAllStations()
        {
            if(BusStations.Count == 0)
            {
                System.Console.WriteLine("Нет актуальных остановок. Создайте их в опции 4 или 7");
                System.Console.WriteLine();
                return;
            }

            for (var i = 0; i < BusStations.Count; i++)
            {
                System.Console.WriteLine();
                System.Console.WriteLine("{0}: Остановка с названием {1}", i + 1, BusStations[i].StationName);
            }
        }

        public static void PrintAllBuses()
        {
            if (Buses.Count == 0)
            {
                System.Console.WriteLine("Автобусы не созданы. Создайте их в опции 3 или 7");
                System.Console.WriteLine();
                return;
            }

            for (var i = 0; i < Buses.Count; i++)
            {
                System.Console.WriteLine();
                System.Console.WriteLine("{0}: Автобус с номером {1}, максимальное число пассажиров - {2}",
                    i + 1, (Buses[i] as Bus).PlateNumber, Buses[i].MaxPassengersCount);

            }
        }

        public static void FindPath()
        {
            System.Console.WriteLine("Выберите станцию отправления");
            PrintAllStations();
            var startStationIndex = int.Parse(System.Console.ReadLine());

            System.Console.WriteLine("Выберите станцию прибытия");
            PrintAllStations();
            var endStationIndex = int.Parse(System.Console.ReadLine());
            var path = PathFinder.FindPath(BusStations[startStationIndex - 1],
                BusStations[endStationIndex - 1], Buses, DateTime.UtcNow);
            var bestRoute = path.Item1[0];
            System.Console.WriteLine("Лучший вариант поездки для вас: Автобус с номером {0}", (bestRoute.Item1 as Bus).PlateNumber);
            System.Console.WriteLine("Он поедет через такие остановки");
            foreach (var station in bestRoute.Item2)
            {
                System.Console.WriteLine("Остановка {0}", station.StationName);
            }

            System.Console.WriteLine("Время в пути: {0}ч:{1}м:{2}с, Средний процент загруженности автобуса: {3}%",
                path.Time.Hours, path.Time.Minutes, path.Time.Seconds, (int)(path.avgMaxPassPercent * 100));
        }

        public static void GenerateRandomRoutes()
        {
            var routes = RouteMaker.MakeNewRoutes(5);
            var stations = RouteMaker.RandomStations.ToList();

            for (int i = 0; i < stations.Count; i++)
            {
                BusStations.Add(stations[i]);
            }
            for (int i = 0; i < routes.Count; i++)
            {
                Routes.Add(routes[i]);
                Buses.Add(routes[i].Transport);
            }
        }
    }
}
