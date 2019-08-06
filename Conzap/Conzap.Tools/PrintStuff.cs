using Conzap;
using Conzap.ObjectPrinting;
using Conzap.ViewStyling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conzap
{
    internal static class PrintStuff
    {
        static string NL = System.Environment.NewLine;

        public static void PrintList( params string[] menuItems)
        {
            var style = GlobalViewStyle.Style;

            for (int i = 1; i <= menuItems.Length; i++)
            {
                string item = menuItems[i - 1];
                var listStyle = "";
                switch (style.ListStyle)
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

                ConzapToolHelpers.WriteLine(listStyle + item);
            }
        }

        public static void PrintLine(string printLine, bool clearScreen = true)
        {
            ConzapToolHelpers.ClearScreen(clearScreen);
            ConzapToolHelpers.WriteLine(printLine);

            ConzapToolHelpers.PrintPostViewWait();
        }

        public static ObjectPrinter<T> PrintObject<T>(T objectToBePrinted)
        {
            var objectPrinter = new ObjectPrinter<T>(new List<T>() { objectToBePrinted });
            return objectPrinter;

        }

        public static ObjectPrinter<T> PrintObjects<T>(List<T> objectsToBePrinted)
        {
            var objectPrinter = new ObjectPrinter<T>( objectsToBePrinted );
            return objectPrinter;
        }
    }
}
