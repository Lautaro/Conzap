using Conzap;
using Conzap.Menu;
using Conzap.ObjectPrinting;
using Conzap.ViewStyling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conzap
{
    public static class ConzapTools
    {
        #region Misc
        /// <summary>
        /// Pauses untill key is pressed. No message.
        /// </summary>
        public static void PauseForKey() => Misc.PauseForKey();

        /// <summary>
        /// Pauses untill specific key is pressed. No message.
        /// </summary>
        public static void PauseForKey(ConsoleKey key) => Misc.PauseForKey(key);

        /// <summary>
        /// Skips amount of lines creating space. Defaults to 2 lines.
        /// </summary>
        public static void SkipLines(int linesToSkip = 2) => Misc.SkipLines(linesToSkip);
        #endregion

        #region Menu
        /// <summary>
        /// Provide a collection of MenuItems and run them directly. 
        /// </summary>
        public static void RunMenu(string header, params ConzapMenuItem[] menuItems) => Misc.RunMenu(menuItems);

        /// <summary>
        /// Create and run a menu from all methods in type that uses the ConzapMenuItemAttribute attribute.
        /// </summary>
        public static void RunMenu(Type menuHolderType) => Misc.RunMenu(menuHolderType);


        /// <summary>
        /// Create and run a menu from all methods in type that uses the ConzapMenuItemAttribute attribute.
        /// Use this overload with an already instanciated instance that has ConzapMenuItems.
        /// </summary>
        public static void RunMenu<T>(T instance) => Misc.RunMenu<T>(instance);

        public static ConzapMenu NewMenu(string heading = null)
        {
            return new ConzapMenu().SetHeading(heading);
        }

        public static ConzapMenu NewMenu(ConzapMenuItem menuItem, string heading = null)
        {
            return new ConzapMenu(menuItems: new List<ConzapMenuItem>() { menuItem }).SetHeading(heading);
        }

        public static ConzapMenu NewMenu(List<ConzapMenuItem> menuItems, string heading = null)
        {
            return new ConzapMenu(menuItems: menuItems).SetHeading(heading);
        }
        #endregion

        #region PrintStuff


        /// <summary>
        /// Prints a list and waits for key to continue
        /// </summary>
        public static void PrintList(params string[] list)
            => PrintStuff.PrintList(list);

        public static void PrintLine(string printLine)
    => PrintStuff.PrintLine(printLine);

        public static ObjectPrinter<T> PrintObject<T>(T objectToBePrinted) => PrintStuff.PrintObject(objectToBePrinted);

        public static ObjectPrinter<T> PrintObjects<T>(List<T> objectsToBePrinted) => PrintStuff.PrintObjects(objectsToBePrinted);

        public static ObjectPrinter<T> PrintCustomObjects<T>(List<T> objectsToBePrinted)
        {
            return PrintStuff.PrintObjects(objectsToBePrinted).Configure(ObjectPrinterOptions.UseOnlyCustomFields);
        }

        public static void PrintHeading(string heading = null, bool clearScreen  = true)
        {
           ConzapToolHelpers.ClearAndPrintHeading(heading, clearScreen);
        }

        #endregion

        #region Choose
        /// <summary>
        /// Stops execution and asks user for a one key input and returns it.
        /// </summary>
        /// <param name="message">Message for the user</param>
        /// <param name="clearScreen">If screen should clear before asking for input</param>
        /// <returns>The one key input as a ConsoleKeyInfo</returns>
        public static ConsoleKeyInfo ChooseKey(string message = "Press any key to continue...", bool clearScreen = false)
        {
            ConzapToolHelpers.ClearScreen(clearScreen);

            ConzapToolHelpers.WriteLine(message);

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
        public static int ChooseInt(string promptMessage, int lowestNr = 1, int highestNumber = 9999999, int modifier = 0)
        => Choose.ChooseInt(promptMessage, lowestNr, highestNumber);

        /// <summary>
        /// Stops execution and asks user for input.
        /// </summary>
        /// <param name="message">Message displayed on screen when asking for input</param>
        /// <param name="clearScreen">Should screen be cleared before asking for input?</param>
        /// <returns>the input as string</returns>
        public static string ChooseString(string message, bool clearScreen = false) => Choose.ChooseString();

        /// <summary>
        /// Prints a list and asks user to choose one of the items. If input doesnt parse to int the user will be asked again.
        /// This overload clears screen before printing.
        /// </summary>
        /// <param name="message">Message displayed on screen when asking for input</param>
        /// <param name="errorMessage">Displayed if input does not parse to in</param>
        /// <param name="clearScreen">Should screen be cleared before asking for input?</param>
        /// <returns>the parsed input as int</returns>
        public static int ChooseFromList(params string[] menuItems)
        {
            return Choose.ChooseFromList(null, menuItems);
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
        public static T ChooseFromList<T>(IEnumerable<T> items, Func<T, string> keyFactory, ViewStyle style= null)
            => Choose.ChooseFromList(items, keyFactory, style);

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

