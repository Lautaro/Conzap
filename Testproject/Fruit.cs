using Conzap;
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
