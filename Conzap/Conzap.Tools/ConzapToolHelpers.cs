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
        internal static void PrintPostViewWait()
        {
            var style = GlobalViewStyle.Style;

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

            WriteLine(style.WaitMessage);

            if (style.WaitKey.HasValue)
            {
                ConzapTools.PauseForKey(style.WaitKey.Value);
            }
            else
            {
                ConzapTools.PauseForKey();
            }
        }

        internal static void ClearAndPrintHeading(string heading, bool clearScreen = true)
        {
            ClearScreen(clearScreen);
            PrintHeading(heading);
        }

        internal static void PrintHeading(string heading)
        {
            var headingStyle = GlobalViewStyle.Style.HeadingStyle;
            if (string.IsNullOrEmpty(heading))
            {
                return;
            }

            var decorationChar = Convert.ToChar(headingStyle.Decoration.Substring(0, 1));
            var indents = 3;
            var headingLength = heading.Length +2;

            if (headingStyle.DecorationType == HeadingDecoration.Wrapped)
            {
                WriteLine(new string(decorationChar, headingLength + indents));
            }

            if (headingStyle.DecorationType == HeadingDecoration.Wrapped || headingStyle.DecorationType == HeadingDecoration.Indentation)
            {
                WriteLine($"{new string(decorationChar,  indents)} {heading}");
            }
            else
            {
                WriteLine($"{heading}");
            }

            if (headingStyle.DecorationType == HeadingDecoration.Wrapped || headingStyle.DecorationType == HeadingDecoration.Underlined)
            {
                WriteLine(new string(decorationChar, headingLength + indents));
            }

            ConzapTools.SkipLines(headingStyle.SkipLines);
        }

        internal static void WriteLine(string writeLine)
        {
            if (string.IsNullOrEmpty(writeLine)) return;
            var padding = GlobalViewStyle.Style.LeftPadding;
            var leftPadding = new string(' ', padding);
            Console.WriteLine(leftPadding + writeLine);
        }

        internal static void Write(string write)
        {
            if (string.IsNullOrEmpty(write)) return;
            var padding = GlobalViewStyle.Style.LeftPadding;
            var leftPadding = new string(' ', padding);
            Console.Write(leftPadding + write);
        }

        internal static void WritePrompt(string writeLine)
        {
            if (string.IsNullOrEmpty(writeLine)) return;

            ConzapTools.SkipLines(1);
            WriteLine(writeLine);
            Write(">>> ");
        }

        internal static void ClearScreen()
        {
            Console.Clear();
            ConzapTools.SkipLines(GlobalViewStyle.Style.TopPadding);
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
                WriteLine(message);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
