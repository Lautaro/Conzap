using System;

namespace Conzap.ObjectPrinting
{
    [AttributeUsage(AttributeTargets.Property| AttributeTargets.Field)]
    public class ObjectPrinterPropertyAttribute : System.Attribute
    {
        public string Title { get; set; }
        public bool Ignore { get; set; }

        public ObjectPrinterPropertyAttribute(bool ignore)
        {
            Ignore = ignore;
        }

        public ObjectPrinterPropertyAttribute(string title)
        {
            Title = title;
        }
    }
}
