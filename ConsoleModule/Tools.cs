using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleModule
{
    class Tools
    {
        static string NL = System.Environment.NewLine;
        public static ConsoleKeyInfo KeyInput(string message = "Press any key to continue...", bool clearScreen = false)
        {
            if (clearScreen)
            {
                Console.Clear();
            }
            Console.WriteLine(message);
            var input = Console.ReadKey();
            return input;
        }

        public static int NumberInput(string message, int firstNumber = 1, int lastNumber = 9999999, string errorMessage = "Invalid number...", bool clearScreen = false)
        {
            while (true)
            {
                if (clearScreen)
                {
                    Console.Clear();
                }

                var input = StringInput(message);
                if (int.TryParse(input, out int number))
                {
                    if (number >= firstNumber && number <= lastNumber)
                    {
                        return number;
                    }
                }

                message = NL + errorMessage;
            }
        }


        public static string StringInput(string message, bool clearScreen = false)
        {
            if (clearScreen)
            {
                Console.Clear();
            }
            Console.WriteLine(message);
            var input = Console.ReadLine();
            return input;
        }


        public static int PrintMenu(params string[] list)
        {
            return PrintMenu(list: list, clearScreen: false);
        }

        public static int PrintMenu(string message, params string[] list)
        {
            return PrintMenu(message, "", "Choose an option...", true, list);
        }


        public static int PrintMenu(string header = "", string message = "Choose an option....", string errorMessage = "", bool clearScreen = true, params string[] list)
        {
            if (clearScreen)
            {
                Console.Clear();
            }
            Tools.PrintList(header, style: ConsoleListStyle.Numbers, list: list);

            var number = NumberInput(message, 1, list.Count());
            return number;
        }

        public static void SkipLines(int linesToSkip = 2)
        {
            for (int i = 0; i < linesToSkip; i++)
            {
                Console.WriteLine();
            }
        }

        public static void PrintList(string header, params string[] list)
        {
            PrintList(header, ConsoleListStyle.None, list);
        }

        public static void PrintList(string header, ConsoleListStyle style, params string[] list)
        {
            if (!string.IsNullOrEmpty(header))
            {
                Console.WriteLine(header);
            }

            for (int i = 1; i <= list.Length; i++)
            {
                string item = list[i - 1];
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
                    case ConsoleListStyle.Asterisc:
                        listStyle = " * ";
                        break;
                    default:
                        break;
                }

                Console.WriteLine(listStyle + item);
            }

            //    DoActionMenu("DO WHAT?",
            //        new ActionMenuItem() { Header = "Do this", Callback = Do1 },
            //        new ActionMenuItem() { Header = "Or you can do that why not", Callback = Do2 },
            //        new ActionMenuItem() { Header = "Last option ", Callback = Do3 });
            //
        }


        public static void Do1()
        {
            Tools.KeyInput("DO1 !!!!");
        }
        public static void Do2()
        {
            Tools.KeyInput("DO2 !!!!");
        }
        public static void Do3()
        {
            Tools.KeyInput("DO3 !!!!");
        }
        public static void DoActionMenu(string header, params ActionMenuItem[] menu)
        {
            while (true)
            {
                var input = PrintMenu(header, clearScreen: true, list: menu.Select(ami => ami.Header).ToArray());
                Console.Clear();
                menu[input - 1].Callback();
            }
        }

        public enum ConsoleListStyle
        {
            None,
            Numbers,
            Indent,
            Hyphen,
            Asterisc
        }



        public class ActionMenuItem
        {
            public string Header { get; set; }
            public Action Callback { get; set; }
        }

        public class ActionMenu
        {
            public List<ActionMenuItem> MenuItems { get; set; }
            public string Header { get; set; }

            public ActionMenu()
            {
                MenuItems = new List<ActionMenuItem>();
            }
        }
    }
}

