﻿using Conzap;
using Conzap.Menu;
using Conzap.Tools;
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

        static void Main(string[] args)
        {

            // Uncomment one of the options to se an example of Conzap

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
            TypePrinter();
        }

        private static void TypePrinter()
        {
            var fruits = GetFruits();
            var printer = new ConzapTypePrinter<Fruit>(fruits);
            printer.Run();

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
                Add("Fruit type: ", f => f.Type + $" ({f.Id})").
                Add("Description: ", f => f.Description).
                Add("Quantity: ", f => f.Quantity.ToString()).
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

        private static void ReflectedConzapMenu()
        {
            ConzapTools.RunMenu(typeof(ReflectedConzapMenu));
        }

        private static List<Fruit> GetFruits()
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

    class Fruit
    {
        static int nextId;

        static int GetNextId()
        {
            return nextId++;
        }

        [ConzapProperty("Identification")]
        public int Id { get; set; }

        [ConzapProperty("Amount of fruits")]
        public int Quantity { get; set; }

        [ConzapProperty("Fruit")]
        public string Type { get; set; }

        [ConzapProperty("About this fruit")]
        public string Description { get; set; }

        [ConzapProperty("Main color")]
        public Color Color { get; set; }

        public Fruit(int quantity, string type, string description, Color color)
        {
            Id = GetNextId();
            Quantity = quantity;
            Type = type;
            Description = description;
            Color = color;
        }
    }
}
