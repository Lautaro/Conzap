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

        public static void PrintList(ViewStyle style = null , params string[] menuItems)
        {   
            ConzapToolHelpers.PrintHeading(style.HeadingStyle);

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

                ConzapToolHelpers.ConsoleWriteLine(listStyle + item);
            }
        }

        public static void PrintLine(string printLine, ViewStyle style)
        {
            ConzapToolHelpers.ClearScreen(style.ClearScreen);
            ConzapToolHelpers.ConsoleWriteLine(printLine);

            ConzapToolHelpers.PrintPostViewWait(style);
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
