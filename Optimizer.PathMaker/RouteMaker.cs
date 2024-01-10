using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Optimizer.Domain.Bus;
using Optimizer.Domain.Bus.Entities;
using Optimizer.Domain.Bus.ValueObjects;
using Optimizer.Domain.Common.Entities;
using Optimizer.Domain.Common.ValueObjects;
using Optimizer.Domain.Route;
using Optimizer.Domain.Route.ValueObjects;


namespace Optimizer.PathMaker.RouteMaker
{
    public class RouteMaker
    {
        public static List<BusStation> stopsList = new List<BusStation>();

        public static Route<Bus,BusId> MakeNewRoute()
        {
            Random random = new Random();

            List<ArrivalTime> arrivalTimes = new List<ArrivalTime>();
            
            var bus = Bus.Create(random.Next(50, 100), PlateNumber.Create("AB" + random.Next(1000, 9999)));
            
            var station = BusStation.Create("Station" + random.Next(1, 10));
            stopsList.Add(station);

            DateTime time = new DateTime(random.Next(2024, 2024), random.Next(1, 12), random.Next(1, 31), random.Next(6, 12), random.Next(0, 60), random.Next(0, 60));
            for (int i = 0; i < 10; i++)
            {

                time = time.AddMinutes(15);
                arrivalTimes.Add(ArrivalTime.Create(station, time));

            }
            var route = Route<Bus, BusId>.Create(bus, arrivalTimes);
            bus.AddRoute(route);
            return route;
        }

        public static List<Route<Bus, BusId>> MakeNewRoutes(int numberOfRoutes)
        {
            var routes = new List<Route<Bus, BusId>>();
            for (int i = 0;i < numberOfRoutes; i++)
            {
                routes.Add(MakeNewRoute());
            }
            return routes;
        }
            
    }
}
