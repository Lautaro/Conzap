using Conzap.Menu;
using Conzap.ViewStyling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Conzap
{
    internal static class Misc
    {
        static string NL = System.Environment.NewLine;

        public static void PauseForKey()
        {
            Console.ReadKey(true);
        }

        public static void PauseForKey(ConsoleKey key)
        {
            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == key))
            {
                // do nothing
            }
        }

        public static void SkipLines(int linesToSkip = 2)
        {
            for (int i = 0; i < linesToSkip; i++)
            {
                Console.WriteLine();
            }
        }

        public static void RunMenu(params ConzapMenuItem[] menuItems)
        {
            var menu = new ConzapMenu(menuItems.ToList());
            menu.Run();
        }

        public static void RunMenu(Type menuHolderType)
        {
            var menu = new ConzapMenu();
            var instance = Activator.CreateInstance(menuHolderType);
            var menuItemAttributedMethods = menuHolderType.GetMethods().
                Where(m => m.GetCustomAttributes(typeof(ConzapMenuItemAttribute), true).Count() > 0);

            foreach (var menuItemMethod in menuItemAttributedMethods)
            {
                ConzapMenuItemAttribute metaData = menuItemMethod.GetCustomAttributes(typeof(ConzapMenuItemAttribute), true)[0] as ConzapMenuItemAttribute;
                var index = metaData.Index;
                var header = metaData.Header;
                var delegateAction = (Action)menuItemMethod.CreateDelegate(typeof(Action), instance);
                menu.Add(header, delegateAction);
            }
            menu.Run();
        }

        public static void RunMenu<T>(T instance)
        {
            var menu = new ConzapMenu();
            var type = typeof(T);
            var menuItemAttributedMethods = type.GetMethods().
                Where(m => m.GetCustomAttributes(typeof(ConzapMenuItemAttribute), true).Count() > 0);

            foreach (var menuItemMethod in menuItemAttributedMethods)
            {
                ConzapMenuItemAttribute metaData = menuItemMethod.GetCustomAttributes(typeof(ConzapMenuItemAttribute), true)[0] as ConzapMenuItemAttribute;
                var index = metaData.Index;
                var header = metaData.Header;
                var delegateAction = (Action)menuItemMethod.CreateDelegate(typeof(Action), instance);
                menu.Add(header, delegateAction);
            }
            menu.Run();
        }

        
    }
}
