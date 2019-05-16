using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conzap
{
    public static class Extensions
    {
        public static void Run(this ConzapMenu menu)
        {
            while (true)
            {
                var input = ConzapTools.PrintMenu(menu.Header, clearScreen: true, list: menu.MenuItems.Select(ami => ami.Header).ToArray());
                Console.Clear();
                menu.MenuItems[input - 1].Callback();
            }
        }

        public static void Add(this ConzapMenu menu, ConzapMenuItem menuItem)
        {
            if (menuItem != null)
            {
                menu.MenuItems.Add(menuItem);
            }

        }

        public static void Add(this ConzapMenu menu, List<ConzapMenuItem> menuItemList)
        {


            if (menuItemList != null && menuItemList.Count > 0)
            {
                menu.MenuItems.AddRange(menuItemList);
            }
        }

        public static void AddAndRun(this ConzapMenu menu, ConzapMenuItem menuItem = null)
        {
            menu.Add(menuItem);
            menu.Run();
        }

        public static void AddAndRun(this ConzapMenu menu, List<ConzapMenuItem> menuItemList = null)
        {
            menu.Add(menuItemList);
            menu.Run();
        }
    }
}
