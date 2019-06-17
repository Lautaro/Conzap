using Conzap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conzap.Tools
{
    internal static class AskFor
    {
        static string NL = System.Environment.NewLine;

        /// <summary>
        /// Stops execution and asks user for a one key input and returns it.
        /// </summary>
        /// <param name="message">Message for the user</param>
        /// <param name="clearScreen">If screen should clear before asking for input</param>
        /// <returns>The one key input as a ConsoleKeyInfo</returns>
        public static ConsoleKeyInfo AskForKey(string message = "Press any key to continue...", bool clearScreen = false)
        {
            ConzapToolHelpers.ClearScreen(clearScreen);

            ConzapToolHelpers.ConsoleWriteLine(message);

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
        public static int AskForInt(string message, int lowestNr = 1, int highestNumber = 9999999, string errorMessage = "Invalid number...", bool clearScreen = false, int modifier = 0)
        {
            var singleDigit = false;
            if (highestNumber < 10)
            {
                singleDigit = true;
            }

            while (true)
            {
                ConzapToolHelpers.ClearScreen(clearScreen);

                string input = "";
                if (singleDigit)
                {
                    input = AskForKey(message).KeyChar.ToString();
                }
                else
                {
                    input = AskForString(message);
                }

                if (int.TryParse(input, out int number))
                {
                    if (number >= lowestNr && number <= highestNumber)
                    {
                        return number + modifier;
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
        public static string AskForString(string message, bool clearScreen = false)
        {
            ConzapToolHelpers.ClearScreen(clearScreen);
            ConzapToolHelpers.ConsoleWriteLine(message);
            var input = Console.ReadLine();
            return input;
        }

        /// <summary>
        /// Prints a list and asks user to choose one of the items. If input doesnt parse to int the user will be asked again.
        /// This overload clears screen before printing.
        /// </summary>
        /// <param name="message">Message displayed on screen when asking for input</param>
        /// <param name="errorMessage">Displayed if input does not parse to in</param>
        /// <param name="clearScreen">Should screen be cleared before asking for input?</param>
        /// <returns>the parsed input as int</returns>
        public static int AskForListChoice(params string[] menuItems)
        {
            return AskForListChoice(listItems: menuItems, clearScreen: false);
        }

        /// <summary>
        ///Prints a list and asks user to choose one of the items. If input doesnt parse to int the user will be asked again.
        /// This overload clears screen before printing.
        /// </summary>
        /// <param name="message">Message displayed on screen when asking for input</param>
        /// <param name="menuItems">Collection of strings making the items in the menu</param>
        /// <returns>the parsed input as int</returns>
        public static int AskForListChoice(string message, params string[] menuItems)
        {
            return AskForListChoice(message, "", "Choose an option...", true, menuItems);
        }

        /// <summary>
        ///Prints a list and asks user to choose one of the items. If input doesnt parse to int the user will be asked again.
        /// </summary>
        /// <param name="message">Message displayed on screen when asking for input</param>
        /// <param name="lowestNr">Lowest accepted number</param>
        /// <param name="highestNumber">Highest accepted number</param>
        /// <param name="errorMessage">Displayed if input does not parse to in</param>
        /// <param name="clearScreen">Should screen be cleared before asking for input? Default is true.</param>
        /// <param name="listItems">Collection of strings making the items in the menu</param>
        /// <returns>the parsed input as int</returns>
        public static int AskForListChoice(string header = "", string message = "Choose an option....", string errorMessage = "", bool clearScreen = true, params string[] listItems)
        {
            ConzapToolHelpers.ClearScreen(clearScreen);
            PrintStuff.PrintList(header, style: ConsoleListStyle.Numbers, menuItems: listItems);

            var number = AskForInt(message, 1, listItems.Count(), modifier: -1);
            return number;
        }

        /// <summary>
        ///Prints a list of the keys in a KeyValuePair collection and asks user to choose one of the items. If input doesnt parse to int the user will be asked again.
        ///Returns the value of the KeyValuePair
        /// </summary>
        /// <param name="items">Collection of KeyValuePair<string, T>. String will be the items the user sees in the list, value will be what is returned</param>
        /// <param name="message">Message displayed on screen when asking for input</param>
        /// <param name="lowestNr">Lowest accepted number</param>
        /// <param name="highestNumber">Highest accepted number</param>
        /// <param name="errorMessage">Displayed if input does not parse to in</param>
        /// <param name="clearScreen">Should screen be cleared before asking for input? Default is true.</param>
        /// <returns>the parsed input as type T</returns>
        public static T AskForListChoice<T>(IEnumerable<KeyValuePair<string, T>> items, string header = "", string message = "Choose an option....", string errorMessage = "", bool clearScreen = true)
        {
            var titles = items.Select(kvp => kvp.Key).ToArray();

            var index = AskForListChoice(listItems: titles, clearScreen: clearScreen);
            return items.ToArray()[index].Value;
        }

        /// <summary>
        ///Prints a list of the items in collection of T and asks user to choose one of the items. 
        ///Returns the T chosen.
        /// </summary>
        /// <param name="items">Collection of T. </param>
        /// <param name="items">Collection of T. </param>
        /// <param name="keyFactory">How to get the key</param>
        /// <param name="lowestNr">Lowest accepted number</param>
        /// <param name="highestNumber">Highest accepted number</param>
        /// <param name="errorMessage">Displayed if input does not parse to in</param>
        /// <param name="clearScreen">Should screen be cleared before asking for input? Default is true.</param>
        /// <returns>The chosen item of type T</returns>
        public static T AskForListChoice<T>(IEnumerable<T> items, Func<T, string> keyFactory, string header = "", string message = "Choose an option....", string errorMessage = "", bool clearScreen = true)
        {
            var titles = items.Select(item => keyFactory(item)).ToArray();

            var index = AskForListChoice(listItems: titles, clearScreen: clearScreen);
            return items.ToArray()[index];
        }

        /// <summary>
        ///Prints a list of the items in collection of T and asks user to choose one of the items. 
        ///Returns the the returnValue for the chosen item chosen.
        /// </summary>
        /// <param name="items">Collection of T. </param>
        /// <param name="keyFactory">How to get the key</param>
        /// <param name="lowestNr">Lowest accepted number</param>
        /// <param name="highestNumber">Highest accepted number</param>
        /// <param name="errorMessage">Displayed if input does not parse to in</param>
        /// <param name="clearScreen">Should screen be cleared before asking for input? Default is true.</param>
        /// <returns>The chosen item of type T</returns>
        public static string AskForListChoice<T>(IEnumerable<T> items, Func<T, string> keyFactory, Func<T, string> returnValueFactory, string header = "", string message = "Choose an option....", string errorMessage = "", bool clearScreen = true)
        {
            var titles = items.Select(item => keyFactory(item)).ToArray();

            var index = AskForListChoice(listItems: titles, clearScreen: clearScreen);
            return keyFactory(items.ToArray()[index]);
        }

    }
}
