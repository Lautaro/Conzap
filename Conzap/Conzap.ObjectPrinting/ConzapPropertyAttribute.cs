using System;

namespace Conzap.ObjectPrinting
{
    [AttributeUsage(AttributeTargets.Property| AttributeTargets.Field)]
    public class ObjectPrinterPropertyAttribute : System.Attribute
    {
        public string Title { get; set; }

        public ObjectPrinterPropertyAttribute(string title)
        {
            Title = title;
        }
    }
}
