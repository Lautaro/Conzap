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
  public  class HandleFruits
    {
        
        public List<Fruit> Fruits { get; set; }

        public HandleFruits()
        {
            Fruits = GetFruits();
        }

        [ConzapMenuItem("Add new fruit", 0)]
        public void CreateFruit()
        {
            ConzapTools.ClearScreen();
            ConzapTools.PrintLine("CREATE NEW FRUIT");
            var fruit = new Fruit();

            fruit.Type = ConzapTools.ChooseString("Type: ");
            fruit.Description = ConzapTools.ChooseString("Description: ");
            fruit.Quantity = ConzapTools.ChooseInt("Amount: ",0,100);

            Fruits.Add(fruit);
        }

        [ConzapMenuItem("List all fruit", 0)]
        public void ListFruit()
        {
            ConzapTools.ClearScreen();
            ConzapTools.PrintObjects(Fruits)
                .Configure(ObjectPrinterOptions.UseOnlyCustomFields)
                .ItemHeadingFactory(f => f.Type)
                .Print();
        }

        public static List<Fruit> GetFruits()
        {
            return new List<Fruit>()
             {
                 new Fruit(3,"Apple", "Red shiny apples", Color.Red,2),
                 new Fruit(10,"Melon", "Traditional big cucumber melon", Color.Green,4),
                 new Fruit(9,"Lemon", "Yellow sour bitter lemon", Color.Yellow,1),
                 new Fruit(23,"Grape", "Sweet tasty grapes", Color.Purple,2)
             };
        }
    }
}
