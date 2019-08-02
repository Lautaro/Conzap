using Conzap;
using Conzap.Menu;
using Conzap.ObjectPrinting;
using Conzap.ViewStyling;
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

            // 1. Print as is. Prints unattributed properties of Fruit using property name and value and uses attribute title for the properties that are attributed
            // ObjectPrinterOne();

            // 2. Configured to ignore non attributed properties as well as ignore attributes
            //Two();

            // 3. Add custom fields, ignore non attributed properties
            // Three();

            // 4. Ignore certain properties
            // Four();

            // 5. Print just one object
            //Five();

            #region old stuff
            InstantiateConzapMenu();

            //PrintListOfObjects();

            //PrintMenuOfObjects();
            

            //TestMenuItemAttribute();
            #endregion
        }



        // 5. Print just one object
        private static void Five()
        {

        }

        // 1. Pass type but no field information
        private static void One()
        {
            ConzapTools.PrintObjects(fruit).Print();

        }

        // 2. Pass type and configure to ignore non attributed properties as well as ignore attributes
        private static void Two()
        {
            ConzapTools.PrintObjects(fruit)
                .Configure(ObjectPrinterOptions.IgnoreUnattributedProperties | ObjectPrinterOptions.IgnoreAttributes)
                .Print();
        }

        // 3. Pass type and add custom fields, ignore unattributed properties
        private static void Three()
        {
            ConzapTools.PrintObjects(fruit)
           .CustomField("Custom field 1", f => "The default descriptiuon is " + f.Description)
           .Configure(ObjectPrinterOptions.IgnoreUnattributedProperties)
           .Print();
        }

        // 4. Pass type but ignore certain properties
        private static void Four()
        {
            ConzapTools.PrintObjects(fruit)
                .IgnoreProperty(f => f.Type)
                .IgnoreProperty(f => f.Description)
                .IgnoreProperty(f => f.Id)
                .IgnoreProperty(f => f.ContainsKernels)
                .Print();
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

        private static void PrintMenuOfObjects()
        {
            while (true)
            {
                //var ObjectMenu = new ConzapObjectMenu<Fruit>(GetFruits(), f => f.Type.ToUpper(), "Choose fruit to see details", clearSreen: true);
                //ObjectMenu.Add("Name: ", f => f.Type);
                //ObjectMenu.Add("Id: ", f => f.Id.ToString());
                //ObjectMenu.AddAndRun("Descrition: ", f => f.Description);
            }
        }

        private static void PrintListOfObjects()
        {
            var fruits = GetFruits();

            //new ConzapObjectPrinter<Fruit>(fruits).
            //    //Add("Fruit type: ", f => f.Type + $" ({f.Id})").
            //    //Add("Description: ", f => f.Description).
            //    //Add("Quantity: ", f => f.Quantity.ToString()).
            //    Run();
        }

        private static void InstantiateConzapMenu()
        {
            ConzapTools.PrintHeading(ViewStyle.New("Menuuu", true)); 
            ConzapTools.NewMenu()
                .Add("Create person", () => handlePersons.CreatePerson())
                .AddAndRun("List persons", () => handlePersons.ListPersons());
        }

        #endregion
    }
}
