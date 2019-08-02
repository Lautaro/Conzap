using Conzap;
using Conzap.ViewStyling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conzap
{
    internal static class Choose
    {
        static string NL = System.Environment.NewLine;

        /// <summary>
        /// Stops execution and asks user for a one key input and returns it.
        /// </summary>
        /// <param name="message">Message for the user</param>
        /// <param name="clearScreen">If screen should clear before asking for input</param>
        /// <returns>The one key input as a ConsoleKeyInfo</returns>
        public static ConsoleKeyInfo ChooseKey(string promptMessage = null)
        {
            ConzapToolHelpers.ConsoleWriteLine(promptMessage);

            var input = Console.ReadKey();
            return input;
        }

        /// <summary>
        /// Asks user for a number. If input doesnt parse to int the user will be asked again.
        /// </summary>
        /// <param name="promptMessage">Message displayed on screen when asking for input</param>
        /// <param name="lowestNr">Lowest accepted number</param>
        /// <param name="highestNumber">Highest accepted number</param>
        /// <param name="errorMessage">Displayed if input does not parse to in</param>
        /// <param name="clearScreen">Should screen be cleared before asking for input?</param>
        /// <returns>the parsed input as int</returns>
        public static int ChooseInt(string promptMessage = null, int lowestNr = 1, int highestNumber = 9999999)
        {
            ConzapToolHelpers.ConsoleWriteLine(promptMessage);
            var singleDigit = false;
            if (highestNumber < 10)
            {
                singleDigit = true;
            }

            while (true)
            {
                string input = "";
                if (singleDigit)
                {
                    input = ChooseKey().KeyChar.ToString();
                }
                else
                {
                    input = ChooseString();
                }

                if (int.TryParse(input, out int number))
                {
                    if (number >= lowestNr && number <= highestNumber)
                    {
                        return number;
                    }
                }

                ConzapToolHelpers.ConsoleWriteLine($"{NL}{input} Not allowed. Chosse between {lowestNr}-{highestNumber}");
            }
        }
             
        /// <summary>
        /// Stops execution and asks user for input.
        /// </summary>
        /// <param name="message">Message displayed on screen when asking for input</param>
        /// <param name="clearScreen">Should screen be cleared before asking for input?</param>
        /// <returns>the input as string</returns>
        public static string ChooseString()
        {
            var input = Console.ReadLine();
            return input;
        }


        public static int ChooseFromList(ViewStyle style = null, string promptMessage = null, params string[] listItems)
        {
            if (promptMessage == null)
            {
                promptMessage = $"> Choose between {1}-{listItems.Count()}";
            }
            
            PrintStuff.PrintList(style, menuItems: listItems);

            var number = ChooseInt(promptMessage, 1, listItems.Count());
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
        public static T ChooseFromList<T>(IEnumerable<KeyValuePair<string, T>> items, ViewStyle style)
        {
            var titles = items.Select(kvp => kvp.Key).ToArray();

            var index = ChooseFromList(listItems: titles, style: style);
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
        public static T ChooseFromList<T>(IEnumerable<T> items, Func<T, string> keyFactory, ViewStyle style)
        {
            var titles = items.Select(item => keyFactory(item)).ToArray();

            var index = ChooseFromList(titles);
            return items.ToArray()[index];
        }
    }
}
