using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO.Pipes;
using Optimizer.Application;
using Optimizer.Domain.Bus;
using Optimizer.Domain.Bus.Entities;
using Optimizer.Domain.Bus.ValueObjects;
using Optimizer.Domain.Common.Entities;
using Optimizer.Domain.Common.ValueObjects;
using Optimizer.Domain.Route;
using Optimizer.Domain.Route.ValueObjects;
using Optimizer.PathMaker.RouteMaker;
using Optimizer.Console.ConsoleLogic;
using Optimizer.Console.Menu;

internal class Program
{
    private static void Main(string[] args)
    {
        var testMenu = new Menu();
        testMenu.Display();
    }  
}