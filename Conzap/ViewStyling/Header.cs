using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conzap.ViewStyling
{
    public class HeadingStyle
    {
        public int SkipLines { get; set; } = 0;
        public string Decoration { get; set; } = "-";
        public HeadingDecoration DecorationType { get; set; } = HeadingDecoration.Underlined;

        public HeadingStyle SetText()
        {
            return this;
        }
    }

    public enum HeadingDecoration
    {
        None, 
        Underlined,
        Wrapped,
        Indentation,
    }
}
