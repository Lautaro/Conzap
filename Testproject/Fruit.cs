using Conzap.ObjectPrinting;
using System.Drawing;

namespace ConsoleApp2
{
    public class Fruit
    {
        static int nextId;

        static int GetNextId()
        {
            return nextId++;
        }

       [ObjectPrinterProperty("-- Identification")]
        public int Id { get; set; }

        [ObjectPrinterProperty("-- Amount of fruits")]
        public int Quantity { get; set; }

        [ObjectPrinterProperty("-- Fruit")]
        public string Type { get; set; }

        [ObjectPrinterProperty("-- About this fruit")]
        public string Description { get; set; }

        [ObjectPrinterProperty("-- Main color")]
        public Color Color { get; set; }

        public Fruit()
        {
            Color = Color.White;
        }

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
