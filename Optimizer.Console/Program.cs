using System.Globalization;
using Optimizer.Application;
using Optimizer.Domain.Bus;
using Optimizer.Domain.Bus.Entities;
using Optimizer.Domain.Bus.ValueObjects;
using Optimizer.Domain.Common.Entities;
using Optimizer.Domain.Common.ValueObjects;
using Optimizer.Domain.Route;
using Optimizer.Domain.Route.ValueObjects;
using Optimizer.PathMaker.RouteMaker;

internal class Program
{
    private const string MainMenu = """
                                    1 - Создать автобус;
                                    2 - Создать остановку;
                                    3 - Создать маршрут автобуса;
                                    4 - Посмотреть все автобусы;
                                    5 - Посмотреть все остановки;
                                    6 - Построить лучший путь;
                                    0 - Выйти;
                                    """;

    private static readonly List<Transport<BusId>> Buses = new();
    private static readonly List<BusStation> BusStations = new();
    private static readonly List<Route<BusId>> Routes = new();
    private static void Main(string[] args)
    {   
        Console.WriteLine(MainMenu);
        var answer = Console.ReadLine();
        while (answer != null && answer != "0")
        {
            if (answer == "1")
            {
                Console.WriteLine("Введите максимальное количество пассажиров");
                var maxPassengersCount = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите знаковый номер автобуса");
                var plateNumber = Console.ReadLine();
                Buses.Add(Bus.Create(maxPassengersCount, PlateNumber.Create(plateNumber)));
                Console.WriteLine("Вы создали новый автобус под номером {0}", plateNumber);
            }
            else if (answer == "2")
            {
                Console.WriteLine("Введите название остановки");
                var busStationName = Console.ReadLine();
                BusStations.Add(BusStation.Create(busStationName));
                Console.WriteLine("Вы создали остановку {0}", busStationName);
            }
            else if (answer == "3")
            {
                if (Buses.Count < 1)
                {
                    Console.WriteLine("Пока не создано автобусов для маршрута");
                    Console.WriteLine(MainMenu);
                    answer = Console.ReadLine();
                    continue;
                }
                
                if (BusStations.Count < 2)
                {   
                    PrintErrorWithText("Пока не создано как минимум двух станций для маршрута");
                    answer = Console.ReadLine();
                    continue;
                }
                
                Console.WriteLine("Выберите автобус для маршрута");
                PrintAllBuses();
                var busNumber = int.Parse(Console.ReadLine());
                if (busNumber < 1 || busNumber > Buses.Count)
                {
                    PrintErrorWithText("Неверный индекс автобуса");
                    answer = Console.ReadLine();
                    continue;
                }
                
                Console.WriteLine("Сколько станций в маршруте?");
                var busStationsCount = int.Parse(Console.ReadLine());
                var arrivalTimes = new List<ArrivalTime>();
                for (var i = 0; i < busStationsCount; i++)
                {
                    Console.WriteLine("Создание маршрута №{0}", i+1);
                    Console.WriteLine("Выберите станцию");
                    PrintAllStations();

                    var busStationIndex = int.Parse(Console.ReadLine());

                    var date = DateTime.UtcNow.AddHours(i+1);
                
                    Console.WriteLine("Введите количество людей на остановке");
                    var peoplesCount = int.Parse(Console.ReadLine());
                    arrivalTimes.Add(ArrivalTime.Create(BusStations[busStationIndex-1], date, peoplesCount));
                }

                var newRoute = Route<BusId>.Create(Buses[busNumber-1], arrivalTimes);
                Routes.Add(Route<BusId>.Create(Buses[busNumber-1], arrivalTimes));
                Buses[busNumber-1].AddRoute(newRoute);
            }
            else if (answer == "4")
            {
                Console.WriteLine();
                PrintAllBuses();
                Console.WriteLine();
            }
            else if (answer == "5")
            {
                Console.WriteLine();
                for(var i = 0; i<BusStations.Count; i++)
                {
                    Console.WriteLine("{0}: Остановка с названием {1}", i+1, BusStations[i].StationName);
                }
                Console.WriteLine();
            }
            else if (answer == "6")
            {
                Console.WriteLine("Выберите станцию отправления");
                PrintAllStations();
                var startStationIndex = int.Parse(Console.ReadLine());
                
                Console.WriteLine("Выберите станцию прибытия");
                PrintAllStations();
                var endStationIndex = int.Parse(Console.ReadLine());
                var path = PathFinder.FindPath(BusStations[startStationIndex-1],
                    BusStations[endStationIndex-1], Buses, DateTime.UtcNow);
                var bestRoute = path.Item1[0];
                Console.WriteLine("Лучший вариант поездки для вас: Автобус с номером {0}", (bestRoute.Item1 as Bus).PlateNumber);
                Console.WriteLine("Он поедет через такие остановки");
                foreach (var station in bestRoute.Item2)
                {
                    Console.WriteLine("Остановка {0}", station.StationName);
                }
                
                Console.WriteLine("Время в пути: {0}ч:{1}м:{2}с, Средний процент загруженности автобуса: {3}%",
                    path.Time.Hours, path.Time.Minutes, path.Time.Seconds, (int)(path.avgMaxPassPercent * 100));
            }
            Console.WriteLine(MainMenu);
            answer = Console.ReadLine();
        }
        Console.WriteLine(answer);
    }

    private static void PrintAllStations()
    {
        for (var i = 0; i < BusStations.Count; i++)
        {
            Console.WriteLine("{0}: Остановка с названием {1}", i + 1, BusStations[i].StationName);
        }
    }

    private static void PrintErrorWithText(string text)
    {
        Console.WriteLine(text);
        Console.WriteLine(MainMenu);
    }

    private static void PrintAllBuses()
    {
        for (var i = 0; i < Buses.Count; i++)
        {
            Console.WriteLine("{0}: Автобус с номером {1}, максимальное число пассажиров - {2}",
                i + 1, (Buses[i] as Bus).PlateNumber, Buses[i].MaxPassengersCount);
        }
    }
}