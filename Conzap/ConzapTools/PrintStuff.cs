using Conzap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conzap.Tools
{
    internal static class PrintStuff
    {
        static string NL = System.Environment.NewLine;

        public static void PrintList(string header, params string[] list)
        {
            PrintList(header, ConsoleListStyle.None, false, list);
        }

        public static void PrintList(string header = "", ConsoleListStyle style = ConsoleListStyle.Asterisk, bool clearScreen = false, params string[] menuItems)
        {
            ConzapToolHelpers.PrintIfNotEmpty(header);

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

                ConzapToolHelpers.ConsoleWriteLine(listStyle + item);
            }
        }

        public static void PrintLine(string printLine, string waitMessage = "Any key to continue", bool clearScreen = false)
        {
            ConzapToolHelpers.ClearScreen(clearScreen);
            ConzapToolHelpers.ConsoleWriteLine(printLine);

            if (ConzapToolHelpers.PrintIfNotEmpty(waitMessage))
            {
                Misc.PauseForKey();
            }
        }

        public static void PrintObjectList<T>(this IEnumerable<ConzapPrintThis<T>> printThese, IEnumerable<T> objects)
        {
            foreach (var item in objects)
            {
                foreach (var printThis in printThese)
                {
                    PrintLine(printThis.Label + printThis.PrintThis(item), null);
                }

              Misc.SkipLines(1);
            }

            Misc.PauseForKey();
        }

        public static void PrintObject<T>(this IEnumerable<ConzapPrintThis<T>> printThese, T item)
        {
        
                foreach (var printThis in printThese)
                {
                    PrintLine(printThis.Label + printThis.PrintThis(item), null);
                }

            Misc.SkipLines(1);
            Misc.PauseForKey();
        }
    }
}
