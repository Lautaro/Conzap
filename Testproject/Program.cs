using Conzap;
using Conzap.Menu;
using Conzap.ObjectPrinting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static HandlePersons handlePersons = new HandlePersons();
        static List<Fruit> fruit;
        static void Main(string[] args)
        {
            fruit = GetFruits();

            // OBJECT PRINTER

            // 1. Pass type but no field information
            ObjectPrinterOne();

            // 2. Pass type and configure to ignore non attributed properties
            ObjectPrinterTwo();

            // 3. Pass type and add custom fields, ignore non attributed properties
            ObjectPrinterThree();


            #region old stuff

            // First example
            // ReflectedConzapMenu();

            // Second example
            // ConzapMenuWithDelegates();

            // Third example
            // InstantiateConzapMenu(handlePersons);

            // Fourth example
            // AskForList2();

            // Fifths example
            // PrintListOfObjects();

            //PrintListOfObjects2();

            // PrintMenuOfObjects();

            // TYPE PRINTER
            //TypePrinter();

            // MenuItemAttribute test
            //TestMenuItemAttribute();
            #endregion
        }

        private static void ObjectPrinterTwo()
        {
          

        }

        private static void ObjectPrinterOne()
        {
            var op = new ObjectPrinter<Fruit>(fruit);
            op.Print();
        }

        private static void ObjectPrinterThree()
        {
        }

        #region old
        //private static void Blä()
        //{
        //    var nutrientsPrinter = Conzap.ObjectPrinter<Nutrients>();

        //    var fruitPrinter = Conzap.ObjectPrinter<Fruit>(MyObject);
        //        .Field(x => x)
        //        .Field(x => x)
        //        .Field(x => x.list )
        //        .Configure();

        //}

        private static void TestMenuItemAttribute()
        {
            var fruitHandler = new HandleFruits();
            var personHandler = new HandlePersons();

            while (true)
            {
                ConzapTools.RunMenu("CHOOSE AN OPTION: ",
                    new ConzapMenuItem("Handle persons ", () => ConzapTools.RunMenu<HandlePersons>(personHandler)),
                    new ConzapMenuItem("Handle fruits ", () => ConzapTools.RunMenu<HandleFruits>(fruitHandler))
                    );
            }
        }

        private static List<Fruit> GetFruits()
        {
            return HandleFruits.GetFruits();
        }

        private static void TypePrinter()
        {
            var fruits = GetFruits();
            var printer = new ObjectPrinter<Fruit>(fruits);
            printer.Print();

        }

        private static void PrintMenuOfObjects()
        {
            while (true)
            {
                var ObjectMenu = new ConzapObjectMenu<Fruit>(GetFruits(), f => f.Type.ToUpper(), "Choose fruit to see details", clearSreen: true);
                ObjectMenu.Add("Name: ", f => f.Type);
                ObjectMenu.Add("Id: ", f => f.Id.ToString());
                ObjectMenu.AddAndRun("Descrition: ", f => f.Description);
            }
        }

        private static void PrintListOfObjects()
        {
            var fruits = GetFruits();

            new ConzapObjectPrinter<Fruit>(fruits).
                //Add("Fruit type: ", f => f.Type + $" ({f.Id})").
                //Add("Description: ", f => f.Description).
                //Add("Quantity: ", f => f.Quantity.ToString()).
                Run();
        }

        private static void AskForList()
        {
            while (true)
            {
                var options = GetFruits().ToDictionary(f => f.Type, f => f.Description);

                var choice = ConzapTools.AskForListChoice<string>(options, clearScreen: true);

                ConzapTools.PrintLine($"You chose: " + choice, clearScreen: true);
            }
        }

        private static void AskForList2()
        {
            while (true)
            {
                var fruits = GetFruits();
                var choice = ConzapTools.AskForListChoice<Fruit>(fruits, f => f.Description, f => f.Type, clearScreen: true);

                ConzapTools.PrintLine(choice + " MMMM! Great choice :D", clearScreen: true);
            }
        }

        private static void InstantiateConzapMenu()
        {
            var menu = new ConzapMenu()
            {
                Header = "Handle Persons",
                MenuItems = new List<ConzapMenuItem>()
                {
                    new ConzapMenuItem()
                    {
                        Title = "Create person",
                        Callback = () =>  handlePersons.CreatePerson()
                    },
                      new ConzapMenuItem()
                    {
                        Title = "List persons",
                        Callback = () =>  handlePersons.ListPersons()
                    }
                }
            };

            menu.Run();
        }

        private static void ConzapMenuWithDelegates()
        {
            ConzapTools.RunMenu("Handle Persons",
                new ConzapMenuItem("Create Person", () => handlePersons.CreatePerson()),
                new ConzapMenuItem("List Persons", () => handlePersons.ListPersons())
                );
        }

        #endregion
    }
}
