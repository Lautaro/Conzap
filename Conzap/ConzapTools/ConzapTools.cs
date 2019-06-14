using Conzap;
using Conzap.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conzap.Tools
{
    public static class ConzapTools
    {
        #region Misc
        /// <summary>
        /// Pauses untill key is pressed. No message.
        /// </summary>
        public static void PauseForKey() => Misc.PauseForKey();

        /// <summary>
        /// Skips amount of lines creating space. Defaults to 2 lines.
        /// </summary>
        public static void SkipLines(int linesToSkip = 2) => Misc.SkipLines(linesToSkip);


        /// <summary>
        /// Provide a collection of MenuItems and run them directly. 
        /// </summary>
        public static void RunMenu(string header, params ConzapMenuItem[] menuItems) => Misc.RunMenu(header, menuItems);

        /// <summary>
        /// Create and run a menu from all methods in type that uses the ConzapMenuItemAttribute attribute.
        /// </summary>
        public static void RunMenu(Type menuHolderType) => Misc.RunMenu(menuHolderType);


        /// <summary>
        /// Create and run a menu from all methods in type that uses the ConzapMenuItemAttribute attribute.
        /// Use this overload with an already instanciated instance that has ConzapMenuItems.
        /// </summary>
        public static void RunMenu<T>(T instance) => Misc.RunMenu<T>(instance);
        #endregion

        #region PrintStuff


        /// <summary>
        /// Prints a list and waits for key to continue
        /// </summary>
        public static void PrintList(string header, params string[] list)
            => PrintStuff.PrintList(header, list);

        /// <summary>
        /// Prints a list and waits for key to continue. Takes a ConsoleListStyle to choose style of list. 
        /// </summary>
        /// <param name="header">Printed before list</param>
        /// <param name="style">Style list is presented in</param>
        /// <param name="clearScreen">Should screen clear before list is printed?</param>
        /// <param name="menuItems">Items to be listed</param>
        public static void PrintList(string header = "", ConsoleListStyle style = ConsoleListStyle.Asterisk, bool clearScreen = false, params string[] menuItems)
            => PrintStuff.PrintList(header, style, clearScreen, menuItems);

        /// <summary>
        /// Prints a single line.
        /// </summary>
        /// <param name="printLine">String to print</param>
        /// <param name="waitMessage">If not null or empty is printed and then waits for key. Is empty or null there is no wait.</param>
        /// <param name="clearScreen">Should screen clear before printing?</param>
        public static void PrintLine(string printLine, string waitMessage = "Any key to continue", bool clearScreen = false)
            => PrintStuff.PrintLine(printLine, waitMessage, clearScreen);
        #endregion

        #region AskFor
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
        /// <param name="modifier">Modifies the result. Used to handle zero based indexes</param>
        /// <returns>the parsed input as int</returns>
        public static int AskForInt(string message, int lowestNr = 1, int highestNumber = 9999999, string errorMessage = "Invalid number...", bool clearScreen = false, int modifier = 0)
        => AskFor.AskForInt(message, lowestNr, highestNumber, errorMessage, clearScreen, modifier);

        /// <summary>
        /// Stops execution and asks user for input.
        /// </summary>
        /// <param name="message">Message displayed on screen when asking for input</param>
        /// <param name="clearScreen">Should screen be cleared before asking for input?</param>
        /// <returns>the input as string</returns>
        public static string AskForString(string message, bool clearScreen = false) => AskFor.AskForString(message, clearScreen);

        /// <summary>
        /// Prints a list and asks user to choose one of the items. If input doesnt parse to int the user will be asked again.
        /// This overload clears screen before printing.
        /// </summary>
        /// <param name="message">Message displayed on screen when asking for input</param>
        /// <param name="errorMessage">Displayed if input does not parse to in</param>
        /// <param name="clearScreen">Should screen be cleared before asking for input?</param>
        /// <returns>the parsed input as int</returns>
        public static int AskForListChoice(params string[] menuItems) => AskFor.AskForListChoice(menuItems);

        /// <summary>
        ///Prints a list and asks user to choose one of the items. If input doesnt parse to int the user will be asked again.
        /// This overload clears screen before printing.
        /// </summary>
        /// <param name="message">Message displayed on screen when asking for input</param>
        /// <param name="menuItems">Collection of strings making the items in the menu</param>
        /// <returns>the parsed input as int</returns>
        public static int AskForListChoice(string message, params string[] menuItems) => AskFor.AskForListChoice(message, menuItems);

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
            => AskFor.AskForListChoice(header, message, errorMessage, clearScreen, listItems);

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
            => AskFor.AskForListChoice(items, header, message, errorMessage, clearScreen);

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
            => AskFor.AskForListChoice(items, keyFactory, header, message, errorMessage, clearScreen);

        /// <summary>
        ///Prints a list of the items in collection of T and asks user to choose one of the items. 
        ///Returns the the returnValue for the chosen item chosen.
        /// </summary>
        /// <param name="items">Collection of T. </param>
        /// <param name="items">Collection of T. </param>
        /// <param name="keyFactory">How to get the key</param>
        /// <param name="lowestNr">Lowest accepted number</param>
        /// <param name="highestNumber">Highest accepted number</param>
        /// <param name="errorMessage">Displayed if input does not parse to in</param>
        /// <param name="clearScreen">Should screen be cleared before asking for input? Default is true.</param>
        /// <returns>The chosen item of type T</returns>
        public static string AskForListChoice<T>(IEnumerable<T> items, Func<T, string> keyFactory, Func<T, string> returnValueFactory, string header = "", string message = "Choose an option....", string errorMessage = "", bool clearScreen = true)
              => AskFor.AskForListChoice(items, keyFactory, returnValueFactory, header, message, errorMessage, clearScreen);
        #endregion


        public static void ClearScreen()
        {
            ConzapToolHelpers.ClearScreen();
        }
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

