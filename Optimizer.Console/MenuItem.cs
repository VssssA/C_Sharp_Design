using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Optimizer.Console.MenuAbstract;

namespace Optimizer.Console.Menu
{
    public sealed class MenuItem
    {
        public long Id { get; }
        public string Description { get; }
        public bool IsVisible { get; private set; }
        private bool IsExitOption { get; set; }

        private readonly AbstractMenu Menu;
        private readonly Action Action;

        public MenuItem(long id, string description = "", Action action = null)
        {
            Id = id;
            Description = description;
            Action = action;
            Show();
        }

        public MenuItem(long id, string description, AbstractMenu menu)
        {
            Id = id;
            Description = description;
            Menu = menu;
            Show();
        }

        public MenuItem Hide()
        {
            IsVisible = false;
            return this;
        }

        public MenuItem Show()
        {
            IsVisible = true;
            return this;
        }

        public MenuItem SetAsExitOption()
        {
            IsExitOption = true;
            return this;
        }

        public bool Run()
        {
            if (Menu != null) Menu.Display();
            else Action?.Invoke();

            return !IsExitOption;
        }

        public override bool Equals(object o)
        {
            if (o == this) return true;
            if (!(o is MenuItem)) return false;
            var menuItem = (MenuItem)o;
            return menuItem.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
