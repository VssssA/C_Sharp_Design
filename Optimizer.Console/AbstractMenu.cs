using Optimizer.Console.Menu;

namespace Optimizer.Console.MenuAbstract
{
    public abstract class AbstractMenu
    {
        private string Title { get; }

        private readonly List<MenuItem> MenuItemsList;

        protected AbstractMenu(string title)
        {
            Title = title;
            MenuItemsList = new List<MenuItem>();
            Init();
        }

        protected abstract void Init();

        protected virtual void UpdateMenuItems() { }

        public void Display()
        {
            var repeat = true;
            while (repeat)
            {
                UpdateMenuItems();
                System.Console.WriteLine(Title);
                for (var i = 0; i < MenuItemsList.Count; i++)
                {
                    if (MenuItemsList[i].IsVisible)
                        System.Console.WriteLine(i + ". " + MenuItemsList[i].Description);
                }

                System.Console.Write("Выберите опцию: ");
                var input = System.Console.ReadLine();

                try
                {
                    var itemIndex = int.Parse(input);
                    var menuItem = MenuItemsList[itemIndex];
                    if (menuItem.IsVisible) repeat = menuItem.Run();
                    else throw new InvalidOperationException();
                }
                catch (FormatException)
                {
                    System.Console.WriteLine("Некорректный ввод, вы должны ввести цифру.");
                    repeat = true;

                }
                catch (ArgumentOutOfRangeException)
                {
                    System.Console.WriteLine($"Некорректный ввод. Опция {input} не существует");
                    repeat = true;
                }

                catch (InvalidOperationException)
                {
                    System.Console.WriteLine($"Некорректный ввод. Опция {input} скрыта.");
                    repeat = true;
                }
            }
        }

        public void AddMenuItem(MenuItem menuItem)
        {
            if (!MenuItemsList.Contains(menuItem)) MenuItemsList.Add(menuItem);
            else throw new ArgumentException($"Меню с таким id {menuItem.Id} уже существует!");
        }

        public void AddHiddenMenuItem(MenuItem menuItem)
        {
            AddMenuItem(menuItem.Hide());
        }

        public void ShowMenuItem(long itemId)
        {
            try
            {
                var menuItem = new MenuItem(itemId);
                var index = MenuItemsList.IndexOf(menuItem);
                MenuItemsList[index].Show();
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentException($"Ошибка отоброжения элемента меню.Элемент меню с id {itemId} не был добавлен в это меню.");
            }
        }

        public void HideMenuItem(long itemId)
        {
            try
            {
                var menuItem = new MenuItem(itemId);
                var index = MenuItemsList.IndexOf(menuItem);
                MenuItemsList[index].Hide();
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentException($"Ошибка с сокрытием элемента меню. Элемент меню с id {itemId} не был добавлен в это меню.");
            }
        }
    }
}
