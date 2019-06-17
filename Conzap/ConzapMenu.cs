using Conzap;
using Conzap.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conzap.Menu
{
    /// <summary>
    /// The root container of a menu. Add menu items to it and run it. 
    /// Use one of three different methods to add menu items and connect the menu items to methods run when item is selected in menu.
    /// </summary>
    public class ConzapMenu
    {
        public List<ConzapMenuItem> MenuItems { get; set; }
        public string Header { get; set; }

        public ConzapMenu(string header = "Options", List<ConzapMenuItem> menuItems = null)
        {
            if (menuItems != null && menuItems.Count > 0)
            {
                MenuItems = menuItems;
            }
            else
            {
                MenuItems = new List<ConzapMenuItem>();
            }
            if (!string.IsNullOrEmpty(header))
            {
                Header = header;
            }
            MenuItems.Insert(0,new ConzapMenuItem() { Title = "Quit", Value = "quit" });
        }

        public void Run()
        {
            while (true)
            {
                var listItems = MenuItems.Select(ami => ami.Title).ToList();
                
                var input = ConzapTools.AskForListChoice(Header, clearScreen: true, listItems: listItems.ToArray());
                Console.Clear();
                var chosenMenuItem = MenuItems[input];
                var value = chosenMenuItem.Value;
                if (value == "quit")
                {
                    break;
                }
                MenuItems[input].Callback();
            }
        }
        /// <summary>
        /// Add menuItem by instanciating one and adding it.
        /// </summary>        
        public void Add(ConzapMenuItem menuItem)
        {
            if (menuItem != null)
            {
                MenuItems.Add(menuItem);
            }
        }

        /// <summary>
        /// Add menuItem by providing header name and a parameter less method action delegate to call when item is selected. You can point to the method directly using lambda.
        /// </summary>        
        public void Add(string header, Action callback)
        {
            var menuItem = new ConzapMenuItem(header, callback);
            if (menuItem != null)
            {
                MenuItems.Add(menuItem);
            }
        }

        /// <summary>
        /// Add a whole list of menu items
        /// </summary>
        public void AddRange(List<ConzapMenuItem> menuItemList)
        {
            if (menuItemList != null && menuItemList.Count > 0)
            {
                MenuItems.AddRange(menuItemList);
            }
        }

        /// <summary>
        /// Add a whole range of menu items each separated by comma.
        /// </summary>
        public void AddRange(params ConzapMenuItem[] menuItemList)
        {
            if (menuItemList != null && menuItemList.Length > 0)
            {
                MenuItems.AddRange(menuItemList);
            }
        }

        /// <summary>
        /// Use to add one last MenuItem and then call Run()
        /// </summary>
        public void AddAndRun(ConzapMenuItem menuItem = null)
        {
            Add(menuItem);
            Run();
        }

        /// <summary>
        /// Use to add range of MenuItems and then call Run()
        /// </summary>
        public void AddAndRun(List<ConzapMenuItem> menuItemList = null)
        {
            AddRange(menuItemList);
            Run();
        }

    }
}
