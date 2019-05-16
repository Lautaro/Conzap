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
            while (true)
            {
                if (clearScreen)
                {
                    Console.Clear();
                }

                var input = StringInput(message);
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

        public static int PrintMenu(params string[] list)
        {
            return PrintMenu(list: list, clearScreen: false);
        }

        public static int PrintMenu(string message, params string[] list)
        {
            return PrintMenu(message, "", "Choose an option...", true, list);
        }

        /// <summary>
        /// Prints a menu and asks user for input Stops execution and asks user for a number. If input doesnt parse to int the user will be asked again.
        /// </summary>
        /// <param name="message">Message displayed on screen when asking for input</param>
        /// <param name="lowestNr">Lowest accepted number</param>
        /// <param name="highestNumber">Highest accepted number</param>
        /// <param name="errorMessage">Displayed if input does not parse to in</param>
        /// <param name="clearScreen">Should screen be cleared before asking for input?</param>
        /// <returns>the parsed input as int</returns>
        public static int PrintMenu(string header = "", string message = "Choose an option....", string errorMessage = "", bool clearScreen = true, params string[] list)
        {
            if (clearScreen)
            {
                Console.Clear();
            }
            ConzapTools.PrintList(header, style: ConsoleListStyle.Numbers, list: list);

            var number = NumberInput(message, 1, list.Count());
            return number;
        }

        public static void SkipLines(int linesToSkip = 2)
        {
            for (int i = 0; i < linesToSkip; i++)
            {
                Console.WriteLine();
            }
        }

        public static void PrintList(string header, params string[] list)
        {
            PrintList(header, ConsoleListStyle.None, list);
        }

        public static void PrintList(string header, ConsoleListStyle style, params string[] list)
        {
            if (!string.IsNullOrEmpty(header))
            {
                Console.WriteLine(header);
            }

            for (int i = 1; i <= list.Length; i++)
            {
                string item = list[i - 1];
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
                    case ConsoleListStyle.Asterisc:
                        listStyle = " * ";
                        break;
                    default:
                        break;
                }

                Console.WriteLine(listStyle + item);
            }
        }

        public static void RunMenu(string Header, params ConzapMenuItem[] menuItems)
        {
            var menu = new ConzapMenu(Header, menuItems.ToList());
            menu.Run();
        }

        public enum ConsoleListStyle
        {
            None,
            Numbers,
            Indent,
            Hyphen,
            Asterisc
        }
    }

}

