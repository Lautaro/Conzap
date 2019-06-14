﻿using Conzap;
using Conzap.Tools;
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
            ConzapTools.PrintLine("CREATE NEW FRUIT", clearScreen: true);
            var fruit = new Fruit();

            fruit.Type = ConzapTools.AskForString("Type: ");
            fruit.Description = ConzapTools.AskForString("Description: ");
            fruit.Quantity = ConzapTools.AskForInt("Amount: ",0,100, errorMessage:"Must be between 0 and 100");

            Fruits.Add(fruit);
        }

        [ConzapMenuItem("List all fruit", 0)]
        public void ListFruit()
        {
            ConzapTools.ClearScreen();
           var menu = new ConzapTypePrinter<Fruit>(Fruits);
            menu.Run();
            ConzapTools.AskForKey();
        }

        public static List<Fruit> GetFruits()
        {
            return new List<Fruit>()
             {
                 new Fruit(3,"Apple", "Red shiny apples", Color.Red),
                 new Fruit(10,"Melon", "Traditional big cucumber melon", Color.Green),
                 new Fruit(9,"Lemon", "Yellow sour bitter lemon", Color.Yellow),
                 new Fruit(23,"Grape", "Sweet tasty grapes", Color.Purple)
             };
        }
    }
}