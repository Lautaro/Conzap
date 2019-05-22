using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conzap
{
    public static class ConzapTools
    {
        static string NL = System.Environment.NewLine;

        /// <summary>
        /// Stops execution and asks user for a one key input and returns it.
        /// </summary>
        /// <param name="message">Message for the user</param>
        /// <param name="clearScreen">If screen should clear before asking for input</param>
        /// <returns>The one key input as a ConsoleKeyInfo</returns>
        public static ConsoleKeyInfo KeyInput(string message = "Press any key to continue...", bool clearScreen = false)
        {
            if (clearScreen)
            {
                Console.Clear();
            }
            Console.WriteLine(message);
            var input = Console.ReadKey();
            return input;
        }

        /// <summary>
        /// Stops execution and asks user for a number. If input doesnt parse to int the user will be asked again.
        /// </summary>
        /// <param name="message">Message displayed on screen when asking for input</param>
        /// <param name="lowestNr">Lowest accepted number</param>
        /// <param name="highestNumber">Highest accepted number</param>
        /// <param name="errorMessage">Displayed if input does not parse to in</param>
        /// <param name="clearScreen">Should screen be cleared before asking for input?</param>
        /// <returns>the parsed input as int</returns>
        public static int NumberInput(string message, int lowestNr = 1, int highestNumber = 9999999, string errorMessage = "Invalid number...", bool clearScreen = false)
        {
            var singleDigit = false;
            if (highestNumber < 10)
            {
                singleDigit = true;
            }

            while (true)
            {
                if (clearScreen)
                {
                    Console.Clear();
                }
                string input = "";
                if (singleDigit)
                {
                    input = KeyInput(message).KeyChar.ToString();
                }
                else
                {
                    input = StringInput(message);
                }
                 
                if (int.TryParse(input, out int number))
                {
                    if (number >= lowestNr && number <= highestNumber)
                    {
                        return number;
                    }
                }

                message = NL + errorMessage;
            }
        }

        /// <summary>
        /// Stops execution and asks user for input.
        /// </summary>
        /// <param name="message">Message displayed on screen when asking for input</param>
        /// <param name="clearScreen">Should screen be cleared before asking for input?</param>
        /// <returns>the input as string</returns>
        public static string StringInput(string message, bool clearScreen = false)
        {
            if (clearScreen)
            {
                Console.Clear();
            }
            Console.WriteLine(message);
            var input = Console.ReadLine();
            return input;
        }

        /// <summary>
        /// Prints a menu and asks user for input Stops execution and asks user for a number. If input doesnt parse to int the user will be asked again.
        /// This overload clears screen before printing.
        /// </summary>
        /// <param name="message">Message displayed on screen when asking for input</param>
        /// <param name="errorMessage">Displayed if input does not parse to in</param>
        /// <param name="clearScreen">Should screen be cleared before asking for input?</param>
        /// <returns>the parsed input as int</returns>
        public static int PrintMenu(params string[] menuItems)
        {
            return PrintMenu(menuItems: menuItems, clearScreen: false);
        }

        /// <summary>
        /// Prints a menu and asks user for input Stops execution and asks user for a number. If input doesnt parse to int the user will be asked again.
        /// This overload clears screen before printing.
        /// </summary>
        /// <param name="message">Message displayed on screen when asking for input</param>
        /// <param name="menuItems">Collection of strings making the items in the menu</param>
        /// <returns>the parsed input as int</returns>
        public static int PrintMenu(string message, params string[] menuItems)
        {
            return PrintMenu(message, "", "Choose an option...", true, menuItems);
        }

        /// <summary>
        /// Prints a menu and asks user for input Stops execution and asks user for a number. If input doesnt parse to int the user will be asked again.
        /// </summary>
        /// <param name="message">Message displayed on screen when asking for input</param>
        /// <param name="lowestNr">Lowest accepted number</param>
        /// <param name="highestNumber">Highest accepted number</param>
        /// <param name="errorMessage">Displayed if input does not parse to in</param>
        /// <param name="clearScreen">Should screen be cleared before asking for input? Default is true.</param>
        /// <param name="menuItems">Collection of strings making the items in the menu</param>
        /// <returns>the parsed input as int</returns>
        public static int PrintMenu(string header = "", string message = "Choose an option....", string errorMessage = "", bool clearScreen = true, params string[] menuItems)
        {
            if (clearScreen)
            {
                Console.Clear();
            }
            ConzapTools.PrintList(header, style: ConsoleListStyle.Numbers, menuItems: menuItems);

            var number = NumberInput(message, 1,menuItems.Count()+1);
            return number;
        }


        /// <summary>
        /// Skips amount of lines creating space. Defaults to 2 lines.
        /// </summary>
        public static void SkipLines(int linesToSkip = 2)
        {
            for (int i = 0; i < linesToSkip; i++)
            {
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Prints a list and waits for key to continue
        /// </summary>
        public static void PrintList(string header, params string[] list)
        {
            PrintList(header, ConsoleListStyle.None,false, list);
        }

        /// <summary>
        /// Prints a list and waits for key to continue. Takes a ConsoleListStyle to choose style of list. 
        /// </summary>
        /// <param name="header">Printed before list</param>
        /// <param name="style">Style list is presented in</param>
        /// <param name="clearScreen">Should screen clear before list is printed?</param>
        /// <param name="menuItems">Items to be listed</param>
        public static void PrintList(string header = "", ConsoleListStyle style = ConsoleListStyle.Asterisk,bool clearScreen = false, params string[] menuItems)
        {
            if (!string.IsNullOrEmpty(header))
            {
                Console.WriteLine(header);
            }

            for (int i = 1; i <= menuItems.Length; i++)
            {
                string item = menuItems[i - 1];
                var listStyle = "";
                switch (style)
                {
                    case ConsoleListStyle.Numbers:
                        listStyle = i.ToString() + ". ";
                        break;
                    case ConsoleListStyle.Indent:
                        listStyle = "   ";
                        break;
                    case ConsoleListStyle.Hyphen:
                        listStyle = " - ";
                        break;
                    case ConsoleListStyle.Asterisk:
                        listStyle = " * ";
                        break;
                    default:
                        break;
                }

                Console.WriteLine(listStyle + item);
            }
        }

        /// <summary>
        /// Provide a collection of MenuItems and run them directly. 
        /// </summary>
        public static void RunMenu(string Header, params ConzapMenuItem[] menuItems)
        {
            var menu = new ConzapMenu(Header, menuItems.ToList());
            menu.Run();
        }

        /// <summary>
        /// Create and run a menu from all methods in type that uses the ConzapMenuItemAttribute attribute.
        /// </summary>
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

        public enum ConsoleListStyle
        {
            None,
            Numbers,
            Indent,
            Hyphen,
            Asterisk
        }
    }

}

