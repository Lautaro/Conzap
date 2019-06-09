using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conzap
{
    internal static class ConzapToolHelpers
    {
        internal static void ConsoleWriteLine(string writeLine)
        {
            Console.WriteLine(writeLine);
        }

        internal static void ClearScreen(bool clearScreen)
        {
            if (clearScreen)
            {
                Console.Clear();
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
