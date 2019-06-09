using System;

namespace Conzap
{
    [AttributeUsage(AttributeTargets.Property| AttributeTargets.Field)]
    public class ConzapPropertyAttribute : System.Attribute
    {
        public string Title { get; set; }
        public bool Ignore { get; set; }

        public ConzapPropertyAttribute(bool ignore)
        {
            Ignore = ignore;
        }

        public ConzapPropertyAttribute(string title)
        {
            Title = title;
        }
    }
}
