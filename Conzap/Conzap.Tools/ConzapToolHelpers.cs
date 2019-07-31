using Conzap.ViewStyling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conzap
{
    internal static class ConzapToolHelpers
    {
        internal static void PrintPostViewWait(ViewStyle style)
        {
            if (style.WaitMessage == "")
            {
                return;
            }

            if (style.WaitKey.HasValue && style.WaitMessage == null) 
            {
                style.WaitMessage = $"Press {style.WaitKey.Value.ToString()} to continue...";
            }

            if (style.WaitMessage == null)
            {   
                style.WaitMessage = "Press any key to continue...";
            }

            ConsoleWriteLine(style.WaitMessage);

            if (style.WaitKey.HasValue)
            {
                ConzapTools.PauseForKey(style.WaitKey.Value);
            }
            else
            {
                ConzapTools.PauseForKey();
            }
        }

        internal static void PrintHeading(HeadingStyle style)
        {
            if (style == null || style.Text == "")
            {
                return;
            }

            var decorationChar = Convert.ToChar(style.Decoration.Substring(0, 1));

            if (style.DecorationType == HeadingDecoration.Wrapped)
            {
                ConsoleWriteLine(new string(decorationChar, 10));
            }

            if (style.DecorationType == HeadingDecoration.Wrapped || style.DecorationType ==  HeadingDecoration.Indentation)
            {
                ConsoleWriteLine($"{new string(decorationChar, 3)} {style.Text}");
            }

            if (style.DecorationType == HeadingDecoration.Wrapped || style.DecorationType == HeadingDecoration.Underlined)
            {
                ConsoleWriteLine(new string(decorationChar, 10));
            }

            ConzapTools.SkipLines(style.SkipLines);
        }

        internal static void ConsoleWriteLine(string writeLine)
        {
            Console.WriteLine(writeLine);
        }

        internal static void ClearScreen()
        {
                Console.Clear();
        }

        internal static void ClearScreen(bool clearScreen)
        {
            if (clearScreen)
            {
                ClearScreen();
            }
        }

        internal static bool PrintIfNotEmpty(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                ConsoleWriteLine(message);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
