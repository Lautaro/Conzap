﻿using Conzap;
using Conzap.ViewStyling;
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
        public List<ConzapMenuItem> MenuItems { get; set; } = new List<ConzapMenuItem>();
        public string Heading { get; set; }
        public ConzapMenu SetHeading(string heading)
        {
            Heading = heading;
            return this;
        }
        public ConzapMenu(List<ConzapMenuItem> menuItems = null, string heading = null)
        {
            var style = GlobalViewStyle.Style;
            AddRange(menuItems);
            MenuItems.Insert(0,new ConzapMenuItem() { Title = style.QuitItemTitle, Value = style.QuitItemTitle });
        }


        public void Run(string heading = null)
        {
            heading = heading ?? Heading;
            while (true)
            {
                var listItems = MenuItems.Select(ami => ami.Title).ToList();

                ConzapToolHelpers.ClearAndPrintHeading(heading);

                var input = ConzapTools.ChooseFromList(listItems.ToArray()) -1;

                ConzapTools.ClearScreen();
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
        public ConzapMenu Add(string heading, Action callback)
        {
            var menuItem = new ConzapMenuItem(heading, callback);
            if (menuItem != null)
            {
                MenuItems.Add(menuItem);
            }

            return this;
        }

        /// <summary>
        /// Add a whole list of menu items
        /// </summary>
        public ConzapMenu AddRange(List<ConzapMenuItem> menuItemList)
        {
            if (menuItemList != null && menuItemList.Count > 0)
            {
                MenuItems.AddRange(menuItemList);
            }
            return this;
        }

        /// <summary>
        /// Add a whole range of menu items each separated by comma.
        /// </summary>
        public ConzapMenu AddRange(params ConzapMenuItem[] menuItemList)
        {
            if (menuItemList != null && menuItemList.Length > 0)
            {
                MenuItems.AddRange(menuItemList);
            }
            return this;
        }

        /// <summary>
        /// Use to add one last MenuItem and then call Run()
        /// </summary>
        public ConzapMenu AddAndRun(ConzapMenuItem menuItem = null)
        {
            Add(menuItem);
            Run();
            return this;
        }

        /// <summary>
        /// Use to add range of MenuItems and then call Run()
        /// </summary>
        public ConzapMenu AddAndRun(List<ConzapMenuItem> menuItemList = null)
        {
            AddRange(menuItemList);
            Run();
            return this;
        }

        public ConzapMenu AddAndRun(string heading, Action callback )
        {
            if (heading == null)
            {
                heading = Heading;
            }
            Add(heading, callback);
            Run();
            return this;
        }

    }
}
