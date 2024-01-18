using Optimizer.Console.MenuAbstract;


namespace Optimizer.Console.Menu
{
    public class Menu : AbstractMenu
    {
        private bool ShowHiddenMenu;

        public Menu() : base("Добро пожаловать в главное меню") { }

        protected override void Init()
        {
            AddMenuItem(new MenuItem(0, "Выход").SetAsExitOption());
            AddMenuItem(new MenuItem(1, "Создать автобус", () => ConsoleLogic.ConsoleLogic.Makebus()));
            AddMenuItem(new MenuItem(2, "Создать остановку", () => ConsoleLogic.ConsoleLogic.MakeStation()));
            AddMenuItem(new MenuItem(3, "Создать маршрут автобуса", () => ConsoleLogic.ConsoleLogic.MakeRoute()));
            AddMenuItem(new MenuItem(4, "Посмотреть все автобусы", () => ConsoleLogic.ConsoleLogic.PrintAllBuses()));
            AddMenuItem(new MenuItem(5, "Посмотреть все остановки", () => ConsoleLogic.ConsoleLogic.PrintAllStations()));
            AddMenuItem(new MenuItem(6, "Построить лучший путь", () => ConsoleLogic.ConsoleLogic.FindPath()));
            AddMenuItem(new MenuItem(7, "Случайная генерация", () => ConsoleLogic.ConsoleLogic.GenerateRandomRoutes()));
        }
    }
}
