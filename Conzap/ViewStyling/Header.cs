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
        public string Text { get; set; } = "-";
        public HeadingDecoration Decoration { get; set; } = HeadingDecoration.Underlined;
    }

    public enum HeadingDecoration
    {
        None, 
        Underlined,
        Wrapped,
        Indentation,
    }
}
