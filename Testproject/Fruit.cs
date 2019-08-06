using Conzap.ObjectPrinting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ConsoleApp2
{
    public class Fruit
    {
        static int nextId;

        static int GetNextId()
        {
            return nextId++;
        }

       [ObjectPrinterProperty("Identification")]
        public int Id { get; set; }

        [ObjectPrinterProperty("Amount of fruits")]
        public int Quantity { get; set; }

        [ObjectPrinterProperty("Type of fruit")]
        public string Type { get; set; }

        [ObjectPrinterProperty("About this fruit")]
        public string Description { get; set; }

        [ObjectPrinterProperty("Main color")]
        public Color Color { get; set; }

        public bool ContainsKernels { get; set; }
        public string OriginCountry { get; set; }
        public List<Nutrition> Nutritions { get; set; } = new List<Nutrition>();
        public Fruit()
        {
            Color = Color.White;
        }

        public Fruit(int quantity, string type, string description, Color color,int nutritions, bool containsKernels = false, string originCountry = "Unknown" )
            
        {
            Id = GetNextId();
            Quantity = quantity;
            Type = type;
            Description = description;
            Color = color;
            ContainsKernels = containsKernels;
            OriginCountry = originCountry;

            Nutritions = Enumerable.Range(0, nutritions).Select( i => {
                var index = new Random().Next(0, 4);
                var name = "";
                switch (index)
                {
                    case 0:
                        name = "Fructose";
                        break;
                    case 1:
                        name = "Fruit acid";
                        break;
                    case 2:
                        name = "Citric acid";
                        break;
                    case 3:
                        name = "Fibers";
                        break;
                    case 4:
                        name = "Crunchy vitamins";
                        break;
                    default:
                        break;
                }

                return new Nutrition(name);
                
            }).ToList();
        }
    }

    public class Nutrition
    {
        public int Amount { get; set; }
        public string Name { get; set; }

        public Nutrition(string name)
        {
            Name = name;
            Amount = new Random().Next(0, 200);
        }
    }
}
